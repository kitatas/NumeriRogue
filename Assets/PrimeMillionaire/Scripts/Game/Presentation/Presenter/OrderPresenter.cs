using ObservableCollections;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class OrderPresenter : IStartable
    {
        private readonly OrderUseCase _orderUseCase;
        private readonly OrderView _orderView;

        public OrderPresenter(OrderUseCase orderUseCase, OrderView orderView)
        {
            _orderUseCase = orderUseCase;
            _orderView = orderView;
        }

        public void Start()
        {
            _orderUseCase.orders
                .ObserveReplace()
                .SubscribeAwait(async (x, token) =>
                {
                    await _orderView.RenderAsync(x.Index, x.NewValue.card, token);
                })
                .AddTo(_orderView);

            Router.Default
                .SubscribeAwait<OrderValueVO>(async (x, context) =>
                {
                    await _orderView.SetAsync(x, context.CancellationToken);
                })
                .AddTo(_orderView);

            _orderView.Init();
        }
    }
}