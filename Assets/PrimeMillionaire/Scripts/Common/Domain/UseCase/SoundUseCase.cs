using System;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IDisposable
    {
        private readonly SoundRepository _soundRepository;
        private readonly Subject<SoundVO> _playBgm;
        private readonly Subject<SoundVO> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
            _playBgm = new Subject<SoundVO>();
            _playSe = new Subject<SoundVO>();

            // TODO: load save data
            _bgmVolume = new ReactiveProperty<float>();
            _seVolume = new ReactiveProperty<float>();
        }

        public Observable<SoundVO> playBgm => _playBgm;
        public Observable<SoundVO> playSe => _playSe;
        public Observable<float> bgmVolume => _bgmVolume;
        public Observable<float> seVolume => _seVolume;

        public void Play(Bgm bgm, float duration = 0.0f)
        {
            var clip = _soundRepository.Find(bgm);
            _playBgm?.OnNext(new SoundVO(clip, duration));
        }

        public void Play(Se se, float duration = 0.0f)
        {
            var clip = _soundRepository.Find(se);
            _playSe?.OnNext(new SoundVO(clip, duration));
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