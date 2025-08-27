using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class StartPresenter : IStartable
    {
        private readonly StartUseCase _startUseCase;
        private readonly StartView _startView;

        public StartPresenter(StartUseCase startUseCase, StartView startView)
        {
            _startUseCase = startUseCase;
            _startView = startView;
        }

        public void Start()
        {
            _startView.pressStart
                .Subscribe(_ => _startUseCase.PressStart())
                .AddTo(_startView);
        }
    }
}