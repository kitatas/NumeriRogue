using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable
    {
        private readonly ISoundUseCase _soundUseCase;
        private readonly ISoundView _soundView;

        public SoundPresenter(ISoundUseCase soundUseCase, ISoundView soundView)
        {
            _soundUseCase = soundUseCase;
            _soundView = soundView;
        }

        public void Initialize()
        {
            _soundUseCase.playBgm
                .Subscribe(_soundView.PlayBgm)
                .AddTo(_soundView.instance);

            _soundUseCase.playSe
                .Subscribe(_soundView.PlaySe)
                .AddTo(_soundView.instance);

            _soundUseCase.isMuteBgm
                .Subscribe(x =>
                {
                    if (x)
                    {
                        _soundView.PauseBgm();
                    }
                    else
                    {
                        _soundView.UnPauseBgm();
                    }
                })
                .AddTo(_soundView.instance);

            _soundUseCase.bgmVolume
                .Subscribe(_soundView.SetBgmVolume)
                .AddTo(_soundView.instance);

            _soundUseCase.seVolume
                .Subscribe(_soundView.SetSeVolume)
                .AddTo(_soundView.instance);
        }
    }
}