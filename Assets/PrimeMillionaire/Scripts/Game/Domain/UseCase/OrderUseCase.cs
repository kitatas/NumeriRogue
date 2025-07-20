using System.Linq;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using ObservableCollections;
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
        private readonly BonusRepository _bonusRepository;
        private readonly NumericRepository _numericRepository;
        private readonly ObservableList<OrderVO> _orders;

        public OrderUseCase(BonusEntity bonusEntity, BuffEntity buffEntity,
            CommunityBattlePtEntity communityBattlePtEntity, BonusRepository bonusRepository,
            NumericRepository numericRepository)
        {
            _bonusEntity = bonusEntity;
            _buffEntity = buffEntity;
            _communityBattlePtEntity = communityBattlePtEntity;
            _bonusRepository = bonusRepository;
            _numericRepository = numericRepository;
            _orders = new ObservableList<OrderVO>(HandConfig.ORDER_NUM)
            {
                new(),
                new(),
                new(),
            };
        }

        public ObservableList<OrderVO> orders => _orders;

        public int Set(CardVO card)
        {
            var orderList = _orders.ToList();
            var o = orderList.Find(x => x.card == card);
            if (o == null)
            {
                // 選択
                var i = orderList.IndexOf(orderList.Find(x => x.card == null));
                _orders[i] = new OrderVO(card);
                return i + 1;
            }
            else
            {
                // 選択解除
                var i = orderList.IndexOf(o);
                _orders[i] = new OrderVO();
                return i + 1;
            }
        }

        public async UniTask RefreshAsync(CancellationToken token)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                _orders[i] = new OrderVO();
            }

            await UniTaskHelper.DelayAsync(1.0f, token);
        }

        public int currentValue => _orders.Any(x => x.card == null)
            ? 0
            : int.Parse(ZString.Concat(_orders.Select(x => x.card.rank)));

        public int currentValueWithBonus => _bonusEntity.CalcOrderValue(currentValue);

        private bool isValueDown => _communityBattlePtEntity.currentValue >= currentValue;

        public void StockBuff()
        {
            // up / down
            _buffEntity.AddRange(_bonusRepository.Find(isValueDown ? BonusType.ValueDown : BonusType.ValueUp));

            // poker hands
            _buffEntity.AddRange(_bonusRepository.Find(OrderHelper.GetPokerHands(_orders).ToBonusType()));

            // odd / even
            _buffEntity.AddRange(_bonusRepository.Find(currentValue.IsEven() ? BonusType.Even : BonusType.Odd));

            // special number
            _buffEntity.AddRange(_bonusRepository.Find(_numericRepository.IsAny(currentValue) ? BonusType.SpecialNumbers : BonusType.NotSpecialNumbers));
            foreach (var bonus in _numericRepository.Finds(currentValue)) _buffEntity.AddRange(_bonusRepository.Find(bonus.type));
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