using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class BonusEntity
    {
        private readonly List<BonusVO> _bonusList;

        public BonusEntity()
        {
            _bonusList = new List<BonusVO>();
        }

        public void Add(BonusVO value)
        {
            if (value.type == BonusType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            _bonusList.Add(value);
        }

        public void Clear() => _bonusList.Clear();

        public int CalcOrderValue(int value)
        {
            return _bonusList
                .OrderBy(x => x.type)
                .Aggregate(value, (current, bonus) => Mathf.CeilToInt(current * bonus.value));
        }

        public List<BonusVO> ToVO() => _bonusList.OrderBy(x => x.type).ToList();
    }
}