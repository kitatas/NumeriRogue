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

        public void Init(VolumeVO volume)
        {
            bgm.value = volume.bgm;
            se.value = volume.se;
        }

        public Observable<float> bgmVolume => bgm.OnValueChangedAsObservable();
        public Observable<float> seVolume => se.OnValueChangedAsObservable();
    }
}