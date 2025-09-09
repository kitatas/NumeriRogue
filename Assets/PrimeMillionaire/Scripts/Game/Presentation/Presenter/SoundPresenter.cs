using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View.Button;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class SoundPresenter : IPostStartable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;

        public SoundPresenter(SoundUseCase soundUseCase, VolumeView volumeView)
        {
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
        }

        public void PostStart()
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

            foreach (var buttonView in Object.FindObjectsByType<BaseButtonView>(FindObjectsSortMode.None))
            {
                buttonView.SetUp(_soundUseCase.Play);
            }
        }
    }
}