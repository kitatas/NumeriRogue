using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class BuffEntity
    {
        private readonly List<SkillType> _total;

        public BuffEntity()
        {
            _total = new List<SkillType>();
        }

        public void Add(SkillType type)
        {
            if (type == SkillType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
            _total.Add(type);
        }

        public void Clear() => _total.Clear();

        public int GetTotalCount(SkillType type)
        {
            return _total.Count(x => x == type);
        }
    }
}