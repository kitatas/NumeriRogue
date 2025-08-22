using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class OrderPresenter : IStartable
    {
        private readonly OrderView _orderView;
        private readonly TableView _tableView;

        public OrderPresenter(OrderView orderView, TableView tableView)
        {
            _orderView = orderView;
            _tableView = tableView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<OrderVO>(async (x, context) =>
                {
                    await _orderView.RenderAsync(x.orderIndex, x.card, context.CancellationToken);

                    if (x.side != Side.None)
                    {
                        await _tableView.RenderOrderNo(x.side, x.handIndex, x.no, context.CancellationToken);
                    }
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
                            throw new RebootExceptionVO(ExceptionConfig.NOT_FOUND_FADE);
                    }
                })
                .AddTo(_orderView);

            _orderView.Init();
        }
    }
}