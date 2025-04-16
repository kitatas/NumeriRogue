using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class HoldSkillEntity
    {
        private readonly List<SkillVO> _skills;

        public HoldSkillEntity()
        {
            _skills = new List<SkillVO>();
        }

        public IEnumerable<SkillVO> all => _skills;
        public int count => _skills.Count;

        public void Init(HoldSkillVO skills)
        {
            _skills.AddRange(skills.skills);
        }

        public void Add(SkillVO skill) => _skills.Add(skill);
        public void Remove(SkillVO skill) => _skills.Remove(skill);

        public HoldSkillVO ToVO() => new(_skills);

        public int GetTotalValue(SkillType type)
        {
            return _skills
                .Where(x => x.skillBase.type == type)
                .Sum(x => x.skillEffect.value);
        }

        public float GetTotalRate(SkillType type)
        {
            return GetTotalValue(type) / 100.0f;
        }
    }
}