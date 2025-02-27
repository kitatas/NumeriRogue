using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public abstract class BaseBattlePtEntity
    {
        private int _value;

        public BaseBattlePtEntity()
        {
            Reset();
        }

        public int currentValue => _value;

        public void Set(int value) => _value = value;

        public void Add(int value) => Set(Mathf.Max(0, _value + value));

        public void Reset() => Set(0);
    }
}