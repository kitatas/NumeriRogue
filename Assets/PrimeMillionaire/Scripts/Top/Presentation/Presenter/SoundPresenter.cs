using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View.Button;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class SoundPresenter : IPostInitializable
    {
        private readonly ISoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;

        public SoundPresenter(ISoundUseCase soundUseCase, VolumeView volumeView)
        {
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
        }

        public void PostInitialize()
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

            _volumeView.releaseHandle
                .Subscribe(_ => _soundUseCase.Play(Se.Decision))
                .AddTo(_volumeView);

            foreach (var buttonView in Object.FindObjectsByType<BaseButtonView>(FindObjectsSortMode.None))
            {
                buttonView.SetUp(_soundUseCase.Play);
            }
        }
    }
}