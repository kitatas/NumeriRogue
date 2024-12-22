using System.Collections.Generic;

namespace PrimeMillionaire.Game.Data.Entity
{
    public abstract class HandEntity
    {
        public readonly List<int> hands;

        public HandEntity()
        {
            hands = new List<int>(HandConfig.MAX_NUM);
        }

        public void Add(int value)
        {
            hands.Add(value);
        }

        public void Clear() => hands.Clear();
    }
}