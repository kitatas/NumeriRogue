using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class VolumePresenter : IStartable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;

        public VolumePresenter(SoundUseCase soundUseCase, VolumeView volumeView)
        {
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
        }

        public void Start()
        {
            _volumeView.Init(_soundUseCase.sound);

            _volumeView.masterVolume
                .Subscribe(_soundUseCase.SetMasterVolume)
                .AddTo(_volumeView);

            _volumeView.bgmVolume
                .Subscribe(_soundUseCase.SetBgmVolume)
                .AddTo(_volumeView);

            _volumeView.seVolume
                .Subscribe(_soundUseCase.SetSeVolume)
                .AddTo(_volumeView);
        }
    }
}