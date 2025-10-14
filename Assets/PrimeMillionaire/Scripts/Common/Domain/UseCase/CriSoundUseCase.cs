using System;
using FastEnumUtility;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class CriSoundUseCase : ISoundUseCase, IDisposable
    {
        private readonly SaveRepository _saveRepository;
        private readonly Subject<AudioVO> _playBgm;
        private readonly Subject<AudioVO> _playSe;
        private readonly ReactiveProperty<bool> _isMuteBgm;
        private readonly ReactiveProperty<VolumeVO> _bgmVolume;
        private readonly ReactiveProperty<VolumeVO> _seVolume;
        private VolumeVO _master;

        public CriSoundUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
            _playBgm = new Subject<AudioVO>();
            _playSe = new Subject<AudioVO>();

            var vo = sound;
            _isMuteBgm = new ReactiveProperty<bool>(vo.isMuteBgm);

            _master = vo.master;
            _bgmVolume = new ReactiveProperty<VolumeVO>(vo.bgm.Multiply(_master));
            _seVolume = new ReactiveProperty<VolumeVO>(vo.se.Multiply(_master));
        }

        public Observable<AudioVO> playBgm => _playBgm;
        public Observable<AudioVO> playSe => _playSe;
        public Observable<bool> isMuteBgm => _isMuteBgm;
        public Observable<VolumeVO> bgmVolume => _bgmVolume;
        public Observable<VolumeVO> seVolume => _seVolume;
        public SoundVO sound => _saveRepository.Load().sound.ToVO();

        public void Play(Bgm bgm, float duration = 0)
        {
            if (bgm == Bgm.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BGM);
            if (sound.isMuteBgm) return;
            _playBgm?.OnNext(new AudioVO(bgm.FastToString(), duration));
        }

        public void Play(Se se, float duration = 0)
        {
            if (se == Se.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SE);
            if (sound.isMuteSe) return;
            _playSe?.OnNext(new AudioVO(se.FastToString(), duration));
        }

        public void SetMasterVolume(VolumeVO value)
        {
            _master = value;
            _saveRepository.SaveMaster(value);

            var vo = sound;
            SetBgmVolume(vo.bgm);
            SetSeVolume(vo.se);
        }

        public void SetBgmVolume(VolumeVO value)
        {
            _bgmVolume.Value = value.Multiply(_master);
            _saveRepository.SaveBgm(value);

            _isMuteBgm.Value = sound.isMuteBgm;
        }

        public void SetSeVolume(VolumeVO value)
        {
            _seVolume.Value = value.Multiply(_master);
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