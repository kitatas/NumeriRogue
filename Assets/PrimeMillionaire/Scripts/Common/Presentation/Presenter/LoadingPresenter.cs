using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class LoadingPresenter : IStartable
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoadingView _loadingView;

        public LoadingPresenter(LoadingUseCase loadingUseCase, LoadingView loadingView)
        {
            _loadingUseCase = loadingUseCase;
            _loadingView = loadingView;
        }

        public void Start()
        {
            _loadingUseCase.isLoad
                .Subscribe(_loadingView.Activate)
                .AddTo(_loadingView);
        }
    }
}