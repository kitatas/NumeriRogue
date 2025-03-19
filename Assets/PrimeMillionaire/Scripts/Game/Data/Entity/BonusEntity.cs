using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;
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

        public void Add(BonusType type)
        {
            if (type == BonusType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            _bonusTypes.Add(type);
        }

        public void Clear() => _bonusTypes.Clear();

        public int CalcOrderValue(int value)
        {
            return _bonusTypes.Aggregate(value, (current, type) => (int)(current * type.ToBonus()));
        }

        public BonusVO ToVO() => new(_bonusTypes);
    }
}