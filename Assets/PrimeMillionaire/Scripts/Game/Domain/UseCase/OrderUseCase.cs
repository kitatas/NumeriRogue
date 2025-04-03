using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UniEx;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly BonusEntity _bonusEntity;
        private readonly BuffEntity _buffEntity;
        private readonly CommunityBattlePtEntity _communityBattlePtEntity;
        private readonly PrimeNumberRepository _primeNumberRepository;
        private readonly ObservableList<OrderVO> _orders;

        public OrderUseCase(BonusEntity bonusEntity, BuffEntity buffEntity, CommunityBattlePtEntity communityBattlePtEntity,
            PrimeNumberRepository primeNumberRepository)
        {
            _bonusEntity = bonusEntity;
            _buffEntity = buffEntity;
            _communityBattlePtEntity = communityBattlePtEntity;
            _primeNumberRepository = primeNumberRepository;
            _orders = new ObservableList<OrderVO>(HandConfig.ORDER_NUM)
            {
                new OrderVO(),
                new OrderVO(),
                new OrderVO(),
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
            : int.Parse(string.Join("", _orders.Select(x => x.card.rank)));

        public int currentValueWithBonus => _bonusEntity.CalcOrderValue(currentValue);

        private bool isPrimeNumber => _primeNumberRepository.IsExist(currentValue);

        private bool isSameNumbers => _orders.GroupBy(x => x.card.rank).Count() == 1;

        private bool isSuitMatch => _orders.Any(x => x.card != null) &&
                                    _orders.Select(x => x.card.suit).GroupBy(x => x).Count() == 1;

        private bool isValueDown => _communityBattlePtEntity.currentValue >= currentValue;

        public void StockBuff()
        {
            if (currentValue.IsEven())
            {
                _buffEntity.Add(SkillType.EvenAtk);
                _buffEntity.Add(SkillType.EvenDef);
            }
            else
            {
                _buffEntity.Add(SkillType.OddAtk);
                _buffEntity.Add(SkillType.OddDef);
            }
        }

        public async UniTask PushValueAsync(CancellationToken token)
        {
            _bonusEntity.Clear();
            if (isPrimeNumber) _bonusEntity.Add(BonusType.PrimeNumber);
            if (isSameNumbers) _bonusEntity.Add(BonusType.SameNumbers);
            if (isSuitMatch) _bonusEntity.Add(BonusType.SuitMatch);
            if (isValueDown) _bonusEntity.Add(BonusType.ValueDown);

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