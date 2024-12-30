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
    }
}