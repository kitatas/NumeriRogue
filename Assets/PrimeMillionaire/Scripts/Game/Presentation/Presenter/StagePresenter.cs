using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class StagePresenter : IPostInitializable
    {
        private readonly BattleView _battleView;

        public StagePresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void PostInitialize()
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