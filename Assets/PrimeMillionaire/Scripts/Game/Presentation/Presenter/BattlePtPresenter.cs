using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePtPresenter : IPostInitializable
    {
        private readonly BattlePtView _battlePtView;

        public BattlePtPresenter(BattlePtView battlePtView)
        {
            _battlePtView = battlePtView;
        }

        public void PostInitialize()
        {

            Router.Default
                .SubscribeAwait<BattlePtVO>(async (x, context) =>
                {
                    var rendering = x.side switch
                    {
                        Side.Player => _battlePtView.RenderPlayer(x.value),
                        Side.Enemy => _battlePtView.RenderEnemy(x.value),
                        _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
                    };
                    await rendering.WithCancellation(context.CancellationToken);
                })
                .AddTo(_battlePtView);
        }
    }
}