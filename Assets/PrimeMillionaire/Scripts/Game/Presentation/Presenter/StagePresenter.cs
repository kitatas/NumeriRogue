using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class StagePresenter : IPostStartable
    {
        private readonly BattleView _battleView;

        public StagePresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void PostStart()
        {
            Router.Default
                .SubscribeAwait<StageVO>(async (x, context) =>
                {
                    await _battleView.RenderStageAsync(x, context.CancellationToken);
                })
                .AddTo(_battleView);
        }
    }
}