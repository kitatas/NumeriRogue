using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class LevelPresenter : IStartable
    {
        private readonly LevelUseCase _levelUseCase;
        private readonly LevelView _levelView;

        public LevelPresenter(LevelUseCase levelUseCase, LevelView levelView)
        {
            _levelUseCase = levelUseCase;
            _levelView = levelView;
        }

        public void Start()
        {
            _levelUseCase.level
                .Subscribe(_levelView.Render)
                .AddTo(_levelView);
        }
    }
}