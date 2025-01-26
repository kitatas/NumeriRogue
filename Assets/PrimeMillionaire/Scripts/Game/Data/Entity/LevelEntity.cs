namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class LevelEntity
    {
        private int _value;

        public LevelEntity()
        {
            Reset();
        }

        public int currentValue => _value;

        public void Set(int value) => _value = value;
        public void Add(int value) => Set(currentValue + value);
        public void Reset() => Set(1);
    }
}