using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class HandPresenter : IPostInitializable
    {
        private readonly OrderHandUseCase _orderHandUseCase;
        private readonly TableView _tableView;

        public HandPresenter(OrderHandUseCase orderHandUseCase, TableView tableView)
        {
            _orderHandUseCase = orderHandUseCase;
            _tableView = tableView;
        }

        public void PostInitialize()
        {
            var isSubscribe = true;

            Router.Default
                .SubscribeAwait<DealHandVO>(async (x, context) =>
                {
                    await _tableView.RenderHandsAsync(x.side, x.hands, context.CancellationToken);

                    if (isSubscribe)
                    {
                        isSubscribe = false;
                        SubscribeOrder();
                    }
                })
                .AddTo(_tableView);

            Router.Default
                .SubscribeAwait<PlayerHandFieldVO>(async (x, context) =>
                {
                    await (x.type switch
                    {
                        DisplayType.Show => _tableView.ActivatePlayerFieldAsync(x.duration, context.CancellationToken),
                        DisplayType.Hide => _tableView.DeactivatePlayerFieldAsync(x.duration, context.CancellationToken),
                        _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_DISPLAY),
                    });
                })
                .AddTo(_tableView);

            Router.Default
                .SubscribeAwait<TrashHandVO>(async (x, context) =>
                {
                    await _tableView.TrashHandsAsync(x.side, x.index, x.duration, context.CancellationToken);
                })
                .AddTo(_tableView);
        }

        private void SubscribeOrder()
        {
            foreach (var order in _tableView.OrderAll(Side.Player))
            {
                order
                    .Where(_ => _orderHandUseCase.isActivate)
                    .SubscribeAwait(async (x, token) =>
                    {
                        await _tableView.OrderHandsAsync(Side.Player, x, token);
                        _orderHandUseCase.Set(x);
                    })
                    .AddTo(_tableView);
            }
        }
    }
}