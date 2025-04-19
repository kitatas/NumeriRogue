using PrimeMillionaire.Common;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgm = default;
        [SerializeField] private Slider se = default;

        public void Init(SoundVO sound)
        {
            bgm.value = sound.bgm.volume;
            se.value = sound.se.volume;
        }

        public Observable<float> bgmVolume => bgm.OnValueChangedAsObservable();
        public Observable<float> seVolume => se.OnValueChangedAsObservable();
    }
}