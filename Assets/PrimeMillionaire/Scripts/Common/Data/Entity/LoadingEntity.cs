namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class LoadingEntity
    {
        public bool value { get; private set; }

        public LoadingEntity()
        {
            value = false;
        }

        public void Set(bool value)
        {
            this.value = value;
        }
    }
}