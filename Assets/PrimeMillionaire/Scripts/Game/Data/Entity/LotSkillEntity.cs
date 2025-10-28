using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class LotSkillEntity
    {
        private readonly List<SkillVO> _skills;

        public LotSkillEntity()
        {
            _skills = new List<SkillVO>();
        }

        public void Set(LotSkillVO skill)
        {
            _skills.AddRange(skill.skills);
        }

        public void Clear()
        {
            _skills.Clear();
        }

        public IEnumerable<PickSkillVO> ToPickVOs()
        {
            return _skills
                .Select((x, i) => new PickSkillVO(i, x));
        }

        public LotSkillVO ToVO() => new(_skills);
    }
}