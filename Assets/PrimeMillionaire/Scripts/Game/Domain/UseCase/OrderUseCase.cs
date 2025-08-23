using System.Linq;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using PrimeMillionaire.Game.Utility;
using UniEx;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly BonusEntity _bonusEntity;
        private readonly BuffEntity _buffEntity;
        private readonly CommunityBattlePtEntity _communityBattlePtEntity;
        private readonly NumericRepository _numericRepository;
        private readonly OrderVO[] _orders;

        public OrderUseCase(BonusEntity bonusEntity, BuffEntity buffEntity,
            CommunityBattlePtEntity communityBattlePtEntity, NumericRepository numericRepository)
        {
            _bonusEntity = bonusEntity;
            _buffEntity = buffEntity;
            _communityBattlePtEntity = communityBattlePtEntity;
            _numericRepository = numericRepository;
            _orders = new OrderVO[]
            {
                new(Side.None, 0),
                new(Side.None, 1),
                new(Side.None, 2),
            };
        }

        public OrderVO[] orders => _orders;

        public void SetOrder(Side side, int orderIndex, int handIndex = -1, CardVO card = null)
        {
            _orders[orderIndex] = new OrderVO(side, orderIndex, handIndex, card);
        }

        public async UniTask SetAsync(Side side, int handIndex, CardVO card, CancellationToken token)
        {
            var orderList = _orders.ToList();
            var o = orderList.Find(x => x.card == card);
            if (o == null)
            {
                // 選択
                var i = orderList.IndexOf(orderList.Find(x => x.card == null));
                SetOrder(side, i, handIndex, card);
                await PublishOrderAsync(i, token);
            }
            else
            {
                // 選択解除
                var i = orderList.IndexOf(o);
                SetOrder(side, i, handIndex);
                await PublishOrderAsync(i, token);
            }
        }

        public async UniTask RefreshAsync(CancellationToken token)
        {
            for (int i = 0; i < _orders.Length; i++)
            {
                SetOrder(Side.None, i);
                await PublishOrderAsync(i, token);
            }

            await UniTaskHelper.DelayAsync(1.0f, token);
        }

        private async UniTask PublishOrderAsync(int index, CancellationToken token)
        {
            await Router.Default.PublishAsync(_orders[index], token);
        }

        public int currentValue => _orders.Any(x => x.card == null)
            ? 0
            : int.Parse(ZString.Concat(_orders.Select(x => x.card.rank)));

        public int currentValueWithBonus => _bonusEntity.CalcOrderValue(currentValue);

        private bool isValueDown => _communityBattlePtEntity.currentValue >= currentValue;

        public void StockBuff()
        {
            // up / down
            _buffEntity.AddRange(_numericRepository.FindTarget(isValueDown ? BonusType.ValueDown : BonusType.ValueUp));

            // poker hands
            var pokerHands = OrderHelper.GetPokerHands(_orders);
            if (pokerHands != PokerHands.HighCard) _buffEntity.AddRange(_numericRepository.FindTarget(BonusType.PokerHands));
            _buffEntity.AddRange(_numericRepository.FindTarget(pokerHands.ToBonusType()));

            // odd / even
            _buffEntity.AddRange(_numericRepository.FindTarget(currentValue.IsEven() ? BonusType.EvenNumber : BonusType.OddNumber));

            // special number
            _buffEntity.AddRange(_numericRepository.FindTarget(_numericRepository.IsAny(currentValue) ? BonusType.SpecialNumbers : BonusType.NotSpecialNumbers));
            foreach (var bonus in _numericRepository.Finds(currentValue)) _buffEntity.AddRange(_numericRepository.FindTarget(bonus.type));
        }

        public async UniTask PushValueAsync(CancellationToken token)
        {
            _bonusEntity.Clear();

            foreach (var bonus in _numericRepository.Finds(currentValue)) _bonusEntity.Add(bonus);
            var pokerHands = OrderHelper.GetPokerHands(_orders);
            if (pokerHands != PokerHands.HighCard) _bonusEntity.Add(_numericRepository.Find(pokerHands.ToBonusType()));

            if (isValueDown) _bonusEntity.Add(_numericRepository.Find(BonusType.ValueDown));

            await Router.Default.PublishAsync(new OrderValueVO(currentValue, _bonusEntity.ToVO()), token);
            await UniTaskHelper.DelayAsync(1.0f, token);

            _communityBattlePtEntity.Set(currentValueWithBonus);
        }

        public async UniTask PublishCommunityBattlePtAsync(CancellationToken token)
        {
            var orderValue = new OrderValueVO(_communityBattlePtEntity.currentValue, _bonusEntity.ToVO());
            await Router.Default.PublishAsync(orderValue, token);
        }

        public async UniTask ShowOrderCardsAsync(float duration, CancellationToken token)
        {
            await FadeOrderCardsAsync(Fade.Out, duration, token);
        }

        public async UniTask HideOrderCardsAsync(float duration, CancellationToken token)
        {
            await FadeOrderCardsAsync(Fade.In, duration, token);
        }

        private async UniTask FadeOrderCardsAsync(Fade fade, float duration, CancellationToken token)
        {
            await Router.Default.PublishAsync(new OrderCardsFadeVO(fade, duration), token);
        }
    }
}