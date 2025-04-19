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

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
            _playBgm = new Subject<SoundVO>();
            _playSe = new Subject<SoundVO>();
        }

        public Observable<SoundVO> playBgm => _playBgm;
        public Observable<SoundVO> playSe => _playSe;

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

        public void Dispose()
        {
            _playBgm?.Dispose();
            _playSe?.Dispose();
        }
    }
}