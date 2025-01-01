using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public abstract class BattlePtEntity
    {
        private int _value;

        public BattlePtEntity()
        {
            Reset();
        }

        public int currentValue => _value;

        private void Set(int value) => _value = value;

        public void Add(int value) => Set(Mathf.Max(0, _value + value));

        public void Reset() => Set(0);
    }
}