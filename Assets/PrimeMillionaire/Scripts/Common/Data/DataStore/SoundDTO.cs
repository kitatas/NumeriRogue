using System;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [Serializable]
    public sealed class SoundDTO
    {
        public VolumeDTO master;
        public VolumeDTO bgm;
        public VolumeDTO se;

        public SoundDTO()
        {
            master = new VolumeDTO();
            bgm = new VolumeDTO();
            se = new VolumeDTO();
        }

        public SoundDTO(SoundVO sound)
        {
            master = new VolumeDTO(sound.master);
            bgm = new VolumeDTO(sound.bgm);
            se = new VolumeDTO(sound.se);
        }

        public SoundVO ToVO() => new(master.ToVO(), bgm.ToVO(), se.ToVO());
    }

    [Serializable]
    public sealed class VolumeDTO
    {
        public float volume;
        public bool isMute;

        public VolumeDTO()
        {
            volume = 0.7f;
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