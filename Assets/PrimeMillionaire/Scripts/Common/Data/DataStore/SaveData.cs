namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveData
    {
        public string uid;
        public ProgressDTO progress;
        public InterruptDTO interrupt;
        public SoundDTO sound;

        public static SaveData Create()
        {
            return new SaveData
            {
                uid = "",
                progress = new ProgressDTO(),
                interrupt = null,
                sound = new SoundDTO(),
            };
        }

        public bool IsEmptyUid()
        {
            return string.IsNullOrEmpty(uid);
        }

        public bool HasProgress()
        {
            return progress != null;
        }

        public bool HasInterrupt()
        {
            if (interrupt == null) return false;
            if (interrupt.player.type == CharacterType.None) return false;
            return true;
        }
    }
}