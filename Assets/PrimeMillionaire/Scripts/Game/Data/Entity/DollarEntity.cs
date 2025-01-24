using System;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class DollarEntity
    {
        private int _value;

        public DollarEntity()
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

        public void Subtract(int value)
        {
            if (!IsEnough(value)) throw new Exception();
            Set(currentValue - value);
        }

        public bool IsEnough(int value) => currentValue >= value;

        public void Reset() => Set(0);
    }
}