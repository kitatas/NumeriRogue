using System.Linq;
using ObservableCollections;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly ObservableList<OrderVO> _orders;

        public OrderUseCase()
        {
            _orders = new ObservableList<OrderVO>(HandConfig.ORDER_NUM)
            {
                new OrderVO(),
                new OrderVO(),
                new OrderVO(),
            };
        }

        public ObservableList<OrderVO> orders => _orders;

        public void Set(CardVO card)
        {
            var orderList = _orders.ToList();
            var o = orderList.Find(x => x.card == card);
            if (o == null)
            {
                // 選択
                var i = orderList.IndexOf(orderList.Find(x => x.card == null));
                _orders[i] = new OrderVO(card);
            }
            else
            {
                // 選択解除
                var i = orderList.IndexOf(o);
                _orders[i] = new OrderVO();
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
    }
}