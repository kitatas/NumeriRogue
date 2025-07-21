using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyCountEntity
    {
        private int _value;

        public EnemyCountEntity()
        {
            Reset();
        }

        public int currentValue => _value;

        public void Set(int value)
        {
            _value = Mathf.Max(0, value);
        }

        public void Add(int value)
        {
            Set(currentValue + value);
        }

        public void Reset() => Set(0);
    }
}