using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class FinishPresenter : IPostStartable
    {
        private readonly FinishView _finishView;

        public FinishPresenter(FinishView finishView)
        {
            _finishView = finishView;
        }

        public void PostStart()
        {
            _finishView.Init();

            Router.Default
                .SubscribeAwait<FinishVO>(async (x, context) =>
                {
                    await _finishView.FadeIn(x.type, UiConfig.TWEEN_DURATION)
                        .WithCancellation(context.CancellationToken);
                })
                .AddTo(_finishView);
        }
    }
}