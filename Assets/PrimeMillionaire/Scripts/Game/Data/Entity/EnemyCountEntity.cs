namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyCountEntity
    {
        private int _value;

        public int currentValue => _value;

        public void Set(int value) => _value = value;
        public void Add(int value) => Set(_value + value);
        public void Reset() => Set(0);
    }
}