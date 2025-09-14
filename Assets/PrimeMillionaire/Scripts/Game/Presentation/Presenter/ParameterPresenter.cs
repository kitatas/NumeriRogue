using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class ParameterPresenter : IPostStartable
    {
        private readonly PlayerParameterView _playerParameterView;
        private readonly EnemyParameterView _enemyParameterView;

        public ParameterPresenter(PlayerParameterView playerParameterView, EnemyParameterView enemyParameterView)
        {
            _playerParameterView = playerParameterView;
            _enemyParameterView = enemyParameterView;
        }

        public void PostStart()
        {
            Router.Default
                .SubscribeAwait<PlayerParameterVO>(async (x, context) =>
                {
                    await _playerParameterView.Render(x, UiConfig.TWEEN_DURATION)
                        .WithCancellation(context.CancellationToken);
                })
                .AddTo(_playerParameterView);

            Router.Default
                .SubscribeAwait<EnemyParameterVO>(async (x, context) =>
                {
                    await _enemyParameterView.Render(x, UiConfig.TWEEN_DURATION)
                        .WithCancellation(context.CancellationToken);
                })
                .AddTo(_enemyParameterView);
        }
    }
}