namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class RetryCountEntity
    {
        private int _value;

        public RetryCountEntity()
        {
            Reset();
        }

        public int currentValue => _value;

        public void Set(int value) => _value = value;
        public void Reset() => Set(0);
        public void Increment() => Set(currentValue + 1);

        public void Update(bool value)
        {
            if (value)
            {
                Increment();
            }
            else
            {
                Set(1);
            }
        }

        public bool IsMaxRetry() => currentValue > 3;
    }
}