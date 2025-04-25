using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmSource = default;
        [SerializeField] private AudioSource seSource = default;

        public void PlayBgm(AudioVO value)
        {
            this.Delay(value.duration, () =>
            {
                bgmSource.clip = value.clip;
                bgmSource.Play();
            });
        }

        public void PlaySe(AudioVO value)
        {
            this.Delay(value.duration, () =>
            {
                //
                seSource.PlayOneShot(value.clip);
            });
        }

        public void UnPauseBgm()
        {
            bgmSource.UnPause();
        }

        public void PauseBgm()
        {
            bgmSource.Pause();
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