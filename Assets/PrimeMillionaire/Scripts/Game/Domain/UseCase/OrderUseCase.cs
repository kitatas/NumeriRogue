using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly BonusEntity _bonusEntity;
        private readonly ObservableList<OrderVO> _orders;

        public OrderUseCase(BonusEntity bonusEntity)
        {
            _bonusEntity = bonusEntity;
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

        private bool isSuitBonus => _orders.Any(x => x.card != null) &&
                                    _orders.Select(x => x.card.suit).GroupBy(x => x).Count() == 1;

        public async UniTask PushValueAsync(CancellationToken token)
        {
            _bonusEntity.Clear();
            if (isSuitBonus) _bonusEntity.Add(BonusType.Suit);

            await Router.Default.PublishAsync(new OrderValueVO(currentValue, _bonusEntity.ToVO()), token);
            await UniTaskHelper.DelayAsync(1.0f, token);
        }
    }
}