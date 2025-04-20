using System;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IDisposable
    {
        private readonly SaveRepository _saveRepository;
        private readonly SoundRepository _soundRepository;
        private readonly Subject<AudioVO> _playBgm;
        private readonly Subject<AudioVO> _playSe;
        private readonly ReactiveProperty<VolumeVO> _bgmVolume;
        private readonly ReactiveProperty<VolumeVO> _seVolume;

        public SoundUseCase(SaveRepository saveRepository, SoundRepository soundRepository)
        {
            _saveRepository = saveRepository;
            _soundRepository = soundRepository;
            _playBgm = new Subject<AudioVO>();
            _playSe = new Subject<AudioVO>();

            _bgmVolume = new ReactiveProperty<VolumeVO>(sound.bgm);
            _seVolume = new ReactiveProperty<VolumeVO>(sound.se);
        }

        public Observable<AudioVO> playBgm => _playBgm;
        public Observable<AudioVO> playSe => _playSe;
        public Observable<VolumeVO> bgmVolume => _bgmVolume;
        public Observable<VolumeVO> seVolume => _seVolume;
        public SoundVO sound => _saveRepository.Load().sound.ToVO();

        public void Play(Bgm bgm, float duration = 0.0f)
        {
            if (sound.bgm.isMute) return;

            var clip = _soundRepository.Find(bgm);
            _playBgm?.OnNext(new AudioVO(clip, duration));
        }

        public void Play(Se se, float duration = 0.0f)
        {
            if (sound.se.isMute) return;

            var clip = _soundRepository.Find(se);
            _playSe?.OnNext(new AudioVO(clip, duration));
        }

        public void SetBgmVolume(VolumeVO value)
        {
            _bgmVolume.Value = value;
            _saveRepository.SaveBgm(value);
        }

        public void SetSeVolume(VolumeVO value)
        {
            _seVolume.Value = value;
            _saveRepository.SaveSe(value);
        }

        public void Dispose()
        {
            _playBgm?.Dispose();
            _playSe?.Dispose();
            _bgmVolume?.Dispose();
            _seVolume?.Dispose();
        }
    }
}