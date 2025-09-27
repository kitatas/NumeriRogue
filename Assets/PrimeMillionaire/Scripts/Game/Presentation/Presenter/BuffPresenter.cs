using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BuffPresenter : IPostInitializable
    {
        private readonly BattleView _battleView;

        public BuffPresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<BuffVO>(async (x, context) =>
                {
                    _battleView.PlayBuff(x);
                    await UniTaskHelper.DelayFrameAsync(20, context.CancellationToken);
                })
                .AddTo(_battleView);
        }
    }
}