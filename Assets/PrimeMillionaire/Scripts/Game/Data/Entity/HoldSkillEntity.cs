using System.Collections.Generic;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class HoldSkillEntity
    {
        private readonly List<SkillVO> _skills;

        public HoldSkillEntity()
        {
            _skills = new List<SkillVO>();
        }

        public void Add(SkillVO skill) => _skills.Add(skill);

        public HoldSkillVO ToVO() => new(_skills);
    }
}