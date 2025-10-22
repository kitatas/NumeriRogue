namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveDTO
    {
        public string uid;
        public InterruptDTO interrupt;
        public SoundDTO sound;

        public SaveDTO()
        {
            this.uid = "";
            this.interrupt = null;
            this.sound = new SoundDTO();
        }

        public SaveDTO(SoundDTO sound)
        {
            this.uid = "";
            this.interrupt = null;
            this.sound = sound;
        }

        public SaveVO ToVO() => new(uid, interrupt?.ToVO(), sound.ToVO());
    }
}