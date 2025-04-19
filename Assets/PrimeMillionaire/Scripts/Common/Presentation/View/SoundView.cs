using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmSource = default;
        [SerializeField] private AudioSource seSource = default;

        public void PlayBgm(SoundVO sound)
        {
            this.Delay(sound.duration, () =>
            {
                bgmSource.clip = sound.clip;
                bgmSource.Play();
            });
        }

        public void PlaySe(SoundVO sound)
        {
            this.Delay(sound.duration, () =>
            {
                //
                seSource.PlayOneShot(sound.clip);
            });
        }
    }
}