using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public interface ISoundView
    {
        MonoBehaviour instance { get; }
        void PlayBgm(AudioVO value);
        void PlaySe(AudioVO value);
        void UnPauseBgm();
        void PauseBgm();
        void SetBgmVolume(VolumeVO value);
        void SetSeVolume(VolumeVO value);
    }
}