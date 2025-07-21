using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class LevelEntity
    {
        private int _value;

        public int currentValue => _value;

        public void Set(int value) => _value = Mathf.Max(1, Random.Range(value - 1, value + 2));
        public void Add(int value) => Set(_value + value);
        public void Reset() => Set(0);
    }
}