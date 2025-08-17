using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View.Button;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class SortPresenter : IStartable
    {
        private readonly HandUseCase _handUseCase;
        private readonly SortButtonView _sortButtonView;

        public SortPresenter(HandUseCase handUseCase, SortButtonView sortButtonView)
        {
            _handUseCase = handUseCase;
            _sortButtonView = sortButtonView;
        }

        public void Start()
        {
            _sortButtonView.push
                .Subscribe(_ => _handUseCase.SwitchSort())
                .AddTo(_sortButtonView);
        }
    }
}