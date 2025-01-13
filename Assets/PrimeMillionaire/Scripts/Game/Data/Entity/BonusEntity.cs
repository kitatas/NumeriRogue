using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Game.Utility;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class BonusEntity
    {
        private readonly List<BonusType> _bonusTypes;

        public BonusEntity()
        {
            _bonusTypes = new List<BonusType>();
        }

        public void Add(BonusType type) => _bonusTypes.Add(type);
        public void Clear() => _bonusTypes.Clear();

        public float totalValue => 1.0f + _bonusTypes.Sum(x => x.ToBonus());

        public BonusVO ToVO() => new(_bonusTypes);
    }
}