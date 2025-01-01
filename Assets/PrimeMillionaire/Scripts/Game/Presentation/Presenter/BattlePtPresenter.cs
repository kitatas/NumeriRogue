using System;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePtPresenter : IStartable
    {
        private readonly BattlePtView _battlePtView;

        public BattlePtPresenter(BattlePtView battlePtView)
        {
            _battlePtView = battlePtView;
        }

        public void Start()
        {

            Router.Default
                .SubscribeAwait<BattlePtVO>(async (x, context) =>
                {
                    var rendering = x.side switch
                    {
                        Side.Player => _battlePtView.RenderPlayer(x.value),
                        Side.Enemy => _battlePtView.RenderEnemy(x.value),
                        _ => throw new Exception(),
                    };
                    await rendering.WithCancellation(context.CancellationToken);
                })
                .AddTo(_battlePtView);
        }
    }
}