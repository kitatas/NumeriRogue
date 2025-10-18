namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveDTO
    {
        public string uid;
        public ProgressDTO progress;
        public InterruptDTO interrupt;
        public SoundDTO sound;

        public SaveDTO()
        {
            this.uid = "";
            this.progress = new ProgressDTO();
            this.interrupt = null;
            this.sound = new SoundDTO();
        }

        public SaveDTO(SoundDTO sound)
        {
            this.uid = "";
            this.progress = new ProgressDTO();
            this.interrupt = null;
            this.sound = sound;
        }

        public SaveVO ToVO() => new(uid, progress.ToVO(), interrupt.ToVO(), sound.ToVO());
    }
}