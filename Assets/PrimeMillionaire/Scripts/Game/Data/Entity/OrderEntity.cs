using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class OrderEntity
    {
        private readonly List<SkillType> _targetSkills;

        public OrderEntity()
        {
            _targetSkills = new List<SkillType>();
        }

        public void Add(SkillType type)
        {
            if (type == SkillType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
            _targetSkills.Add(type);
        }

        public void Clear() => _targetSkills.Clear();

        public int GetTotalCount(SkillType type)
        {
            return _targetSkills.Count(x => x == type);
        }
    }
}