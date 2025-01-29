using System.Collections.Generic;
using System.Linq;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class HoldSkillEntity
    {
        private readonly List<SkillVO> _skills;

        public HoldSkillEntity()
        {
            _skills = new List<SkillVO>();
        }

        public int count => _skills.Count;

        public void Add(SkillVO skill) => _skills.Add(skill);
        public void Remove(SkillVO skill) => _skills.Remove(skill);

        public HoldSkillVO ToVO() => new(_skills);

        public float GetTotalRate(SkillType type)
        {
            var totalValue = _skills
                .Where(x => x.type == type)
                .Sum(x => x.value);

            return totalValue / 100.0f;
        }
    }
}