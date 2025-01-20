using System;
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

            Router.Default
                .SubscribeAwait<OrderCardsFadeVO>(async (x, context) =>
                {
                    switch (x.fade)
                    {
                        case Fade.In:
                            await _orderView.FadeInCardsAsync(x.duration, context.CancellationToken);
                            break;
                        case Fade.Out:
                            await _orderView.FadeOutCardsAsync(x.duration, context.CancellationToken);
                            break;
                        default:
                            throw new Exception();
                    }
                })
                .AddTo(_orderView);

            _orderView.Init();
        }
    }
}