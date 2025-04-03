using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BuffPresenter : IStartable
    {
        private readonly BattleView _battleView;

        public BuffPresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<BuffVO>(async (x, context) =>
                {
                    if (x.isActivate)
                    {
                        await UniTaskHelper.DelayAsync(UiConfig.TWEEN_DURATION,  context.CancellationToken);
                        _battleView.PlayBuff();
                    }
                })
                .AddTo(_battleView);
        }
    }
}