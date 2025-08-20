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
        }
    }
}