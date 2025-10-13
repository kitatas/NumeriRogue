using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public interface ISoundUseCase
    {
        Observable<AudioVO> playBgm { get; }
        Observable<AudioVO> playSe { get; }
        Observable<bool> isMuteBgm { get; }
        Observable<VolumeVO> bgmVolume { get; }
        Observable<VolumeVO> seVolume { get; }
        SoundVO sound { get; }
        void Play(Bgm bgm, float duration = 0.0f);
        void Play(Se se, float duration = 0.0f);
        void SetMasterVolume(VolumeVO value);
        void SetBgmVolume(VolumeVO value);
        void SetSeVolume(VolumeVO value);
    }
}