using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class SoundView : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmSource = default;

        public void PlayBgm(SoundVO sound)
        {
            this.Delay(sound.duration, () =>
            {
                bgmSource.clip = sound.clip;
                bgmSource.Play();
            });
        }
    }
}