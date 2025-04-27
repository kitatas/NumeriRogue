using PrimeMillionaire.Common;
using PrimeMillionaire.Top.Presentation.View.Button;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
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
        }

        public Observable<VolumeVO> masterVolume => master.OnValueChangedAsObservable()
            .Skip(1)
            .Select(x =>
            {
                masterMute.Set(false);
                return new VolumeVO(x, masterMute.isMute);
            })
            .Merge(masterMute.push
                .Select(_ =>
                {
                    masterMute.Switch();
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
    }
}