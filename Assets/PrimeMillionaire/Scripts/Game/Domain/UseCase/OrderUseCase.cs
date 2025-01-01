using System.Linq;
using ObservableCollections;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly ObservableList<OrderVO> _orders;
        private readonly ReactiveProperty<int> _orderValue;

        public OrderUseCase()
        {
            _orders = new ObservableList<OrderVO>(HandConfig.ORDER_NUM)
            {
                new OrderVO(),
                new OrderVO(),
                new OrderVO(),
            };

            _orderValue = new ReactiveProperty<int>(0);
        }

        public ObservableList<OrderVO> orders => _orders;
        public Observable<int> orderValue => _orderValue;

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

        public void Refresh()
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                _orders[i] = new OrderVO();
            }
        }

        public int value => _orders.Any(x => x.card == null)
            ? 0
            : int.Parse(string.Join("", _orders.Select(x => x.card.rank)));

        public void PushValue() => _orderValue.Value = value;
    }
}