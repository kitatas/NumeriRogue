using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class ParameterPresenter : IStartable
    {
        private readonly PlayerParameterView _playerParameterView;

        public ParameterPresenter(PlayerParameterView playerParameterView)
        {
            _playerParameterView = playerParameterView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<ParameterVO>(async (x, context) =>
                {
                    await _playerParameterView.Render(x, BattleConfig.TWEEN_DURATION)
                        .WithCancellation(context.CancellationToken);
                })
                .AddTo(_playerParameterView);
        }
    }
}