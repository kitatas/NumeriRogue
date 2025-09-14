using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class EnemyCountPresenter : IPostStartable
    {
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly EnemyCountView _enemyCountView;

        public EnemyCountPresenter(EnemyCountUseCase enemyCountUseCase, EnemyCountView enemyCountView)
        {
            _enemyCountUseCase = enemyCountUseCase;
            _enemyCountView = enemyCountView;
        }

        public void PostStart()
        {
            _enemyCountUseCase.enemyCount
                .Subscribe(_enemyCountView.Render)
                .AddTo(_enemyCountView);
        }
    }
}