using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly SoundView _soundView;

        public SoundPresenter(SoundUseCase soundUseCase, SoundView soundView)
        {
            _soundUseCase = soundUseCase;
            _soundView = soundView;
        }

        public void Initialize()
        {
            _soundUseCase.playBgm
                .Subscribe(_soundView.PlayBgm)
                .AddTo(_soundView);

            _soundUseCase.playSe
                .Subscribe(_soundView.PlaySe)
                .AddTo(_soundView);
        }
    }
}