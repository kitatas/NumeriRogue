using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class InterruptPresenter : IStartable
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly InterruptView _interruptView;

        public InterruptPresenter(InterruptUseCase interruptUseCase, InterruptView interruptView)
        {
            _interruptUseCase = interruptUseCase;
            _interruptView = interruptView;
        }

        public void Start()
        {
            _interruptView.pressDecision
                .Merge(_interruptView.pressCancel)
                .Subscribe(_interruptUseCase.PressButton)
                .AddTo(_interruptView);
        }
    }
}