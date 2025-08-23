using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class HandPresenter : IStartable
    {
        private readonly TableView _tableView;

        public HandPresenter(TableView tableView)
        {
            _tableView = tableView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<DealHandVO>(async (x, context) =>
                {
                    await _tableView.RenderHandsAsync(x.side, x.hands, context.CancellationToken);
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
    }
}