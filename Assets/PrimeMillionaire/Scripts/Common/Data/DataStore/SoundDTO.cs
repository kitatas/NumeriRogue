using System;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [Serializable]
    public sealed class SoundDTO
    {
        public VolumeDTO bgm;
        public VolumeDTO se;

        public SoundDTO()
        {
            bgm = new VolumeDTO();
            se = new VolumeDTO();
        }

        public SoundVO ToVO() => new(bgm.ToVO(), se.ToVO());
    }

    [Serializable]
    public sealed class VolumeDTO
    {
        public float volume;
        public bool isMute;

        public VolumeDTO()
        {
            volume = 0.5f;
            isMute = false;
        }

        public VolumeDTO(VolumeVO volume)
        {
            this.volume = volume.volume;
            this.isMute = volume.isMute;
        }

        public VolumeVO ToVO() => new(volume, isMute);
    }
}