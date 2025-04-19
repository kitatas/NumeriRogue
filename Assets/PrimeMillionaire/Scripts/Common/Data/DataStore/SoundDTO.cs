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

        public VolumeDTO()
        {
            volume = 0.5f;
        }

        public VolumeVO ToVO() => new(volume);
    }
}