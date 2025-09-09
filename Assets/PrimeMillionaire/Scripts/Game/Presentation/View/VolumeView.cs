using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View.Button;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider master = default;
        [SerializeField] private Slider bgm = default;
        [SerializeField] private Slider se = default;
        [SerializeField] private MuteButtonView masterMute = default;
        [SerializeField] private MuteButtonView bgmMute = default;
        [SerializeField] private MuteButtonView seMute = default;

        public void Init(SoundVO sound)
        {
            master.value = sound.master.volume;
            bgm.value = sound.bgm.volume;
            se.value = sound.se.volume;
            masterMute.Init(sound.master);
            bgmMute.Init(sound.bgm);
            seMute.Init(sound.se);
            SetInteractable(!masterMute.isMute);
        }

        public Observable<VolumeVO> masterVolume => master.OnValueChangedAsObservable()
            .Skip(1)
            .Select(x =>
            {
                masterMute.Set(false);
                SetInteractable(!masterMute.isMute);
                return new VolumeVO(x, masterMute.isMute);
            })
            .Merge(masterMute.push
                .Select(_ =>
                {
                    masterMute.Switch();
                    SetInteractable(!masterMute.isMute);
                    return new VolumeVO(master.value, masterMute.isMute);
                })
            );

        public Observable<VolumeVO> bgmVolume => bgm.OnValueChangedAsObservable()
            .Skip(1)
            .Select(x =>
            {
                bgmMute.Set(false);
                return new VolumeVO(x, bgmMute.isMute);
            })
            .Merge(bgmMute.push
                .Select(_ =>
                {
                    bgmMute.Switch();
                    return new VolumeVO(bgm.value, bgmMute.isMute);
                })
            );

        public Observable<VolumeVO> seVolume => se.OnValueChangedAsObservable()
            .Skip(1)
            .Select(x =>
            {
                seMute.Set(false);
                return new VolumeVO(x, seMute.isMute);
            })
            .Merge(seMute.push
                .Select(_ =>
                {
                    seMute.Switch();
                    return new VolumeVO(se.value, seMute.isMute);
                })
            );

        public Observable<Unit> releaseHandle => master.OnPointerUpAsObservable()
            .Merge(bgm.OnPointerUpAsObservable())
            .Merge(se.OnPointerUpAsObservable())
            .Select(_ => Unit.Default);

        private void SetInteractable(bool value)
        {
            bgm.interactable = value;
            se.interactable = value;
            bgmMute.SetInteractable(value);
            seMute.SetInteractable(value);
        }
    }
}