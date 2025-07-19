using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class BuffEntity
    {
        private readonly List<SkillType> _current;
        private readonly List<SkillType> _total;

        public BuffEntity()
        {
            _current = new List<SkillType>();
            _total = new List<SkillType>();
        }

        public void Add(SkillType type)
        {
            if (type == SkillType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
            _current.Add(type);
        }

        public void AddRange(BonusTargetVO bonusTarget)
        {
            if (bonusTarget.skillTypes.Any(x => x == SkillType.None)) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
            _current.AddRange(bonusTarget.skillTypes);
        }

        public void ApplyTotal()
        {
            _total.AddRange(_current);
            _current.Clear();
        }

        public void Clear() => _total.Clear();

        public bool HasAnyCurrent(SkillType type) => _current.Any(s => s == type);

        public int GetTotalCount(SkillType type)
        {
            return _total.Count(x => x == type);
        }
    }
}