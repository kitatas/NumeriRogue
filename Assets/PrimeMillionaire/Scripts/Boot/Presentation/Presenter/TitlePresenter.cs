using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class TitlePresenter : IStartable
    {
        private readonly TitleUseCase _titleUseCase;
        private readonly TitleView _titleView;

        public TitlePresenter(TitleUseCase titleUseCase, TitleView titleView)
        {
            _titleUseCase = titleUseCase;
            _titleView = titleView;
        }

        public void Start()
        {
            _titleView.Init();

            _titleView.push
                .Subscribe(_ => _titleUseCase.TouchScreen())
                .AddTo(_titleView);
        }
    }
}