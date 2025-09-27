using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class DollarPresenter : IPostInitializable
    {
        private readonly DollarUseCase _dollarUseCase;
        private readonly DollarView _dollarView;

        public DollarPresenter(DollarUseCase dollarUseCase, DollarView dollarView)
        {
            _dollarUseCase = dollarUseCase;
            _dollarView = dollarView;
        }

        public void PostInitialize()
        {
            _dollarUseCase.dollar
                .Pairwise()
                .Subscribe(x => _dollarView.Render(x.Previous, x.Current))
                .AddTo(_dollarView);
        }
    }
}