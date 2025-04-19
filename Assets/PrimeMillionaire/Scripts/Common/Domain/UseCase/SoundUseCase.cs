using System;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IDisposable
    {
        private readonly SoundRepository _soundRepository;
        private readonly Subject<AudioVO> _playBgm;
        private readonly Subject<AudioVO> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
            _playBgm = new Subject<AudioVO>();
            _playSe = new Subject<AudioVO>();

            // TODO: load save data
            _bgmVolume = new ReactiveProperty<float>();
            _seVolume = new ReactiveProperty<float>();
        }

        public Observable<AudioVO> playBgm => _playBgm;
        public Observable<AudioVO> playSe => _playSe;
        public Observable<float> bgmVolume => _bgmVolume;
        public Observable<float> seVolume => _seVolume;
        public VolumeVO volume => new(_bgmVolume.Value, _seVolume.Value);

        public void Play(Bgm bgm, float duration = 0.0f)
        {
            var clip = _soundRepository.Find(bgm);
            _playBgm?.OnNext(new AudioVO(clip, duration));
        }

        public void Play(Se se, float duration = 0.0f)
        {
            var clip = _soundRepository.Find(se);
            _playSe?.OnNext(new AudioVO(clip, duration));
        }

        public void SetBgmVolume(float value)
        {
            _bgmVolume.Value = value;
        }

        public void SetSeVolume(float value)
        {
            _seVolume.Value = value;
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