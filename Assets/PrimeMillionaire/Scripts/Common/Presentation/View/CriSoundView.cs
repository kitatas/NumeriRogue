using CriWare;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class CriSoundView : MonoBehaviour, ISoundView
    {
        [SerializeField] private CriAtomSource bgmSource = default;
        [SerializeField] private CriAtomSource seSource = default;

        public MonoBehaviour instance => this;

        public void PlayBgm(AudioVO value)
        {
            if (bgmSource.cueName == value.cueName) return;

            this.Delay(value.duration, () =>
            {
                bgmSource.cueName = value.cueName;
                bgmSource.Play();
            });
        }

        public void PlaySe(AudioVO value)
        {
            this.Delay(value.duration, () =>
            {
                seSource.cueName = value.cueName;
                seSource.Play();
            });
        }

        public void UnPauseBgm()
        {
            bgmSource.Pause(false);
        }

        public void PauseBgm()
        {
            bgmSource.Pause(true);
        }

        public void SetBgmVolume(VolumeVO value)
        {
            bgmSource.volume = value.volume;
        }

        public void SetSeVolume(VolumeVO value)
        {
            seSource.volume = value.volume;
        }
    }
}