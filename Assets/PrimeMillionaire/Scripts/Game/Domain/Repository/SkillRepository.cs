using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class SkillRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public SkillRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public LotSkillVO FindLotteryTargets(int level)
        {
            var random = new System.Random();
            var skills = _memoryDbData.Get().SkillEffectMasterTable.FindClosestByLevel(level)
                .OrderBy(_ => random.Next())
                .Take(SkillConfig.LOT_NUM)
                .Select(skillEffect =>
                {
                    if (_memoryDbData.Get().SkillBaseMasterTable.TryFindByType(skillEffect.Type, out var skillBase))
                    {
                        return new SkillVO(skillBase.ToVO(), skillEffect.ToVO());
                    }
                    else
                    {
                        throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
                    }
                })
                .ToList();

            return new LotSkillVO(skills);
        }

        public bool IsExistTarget(SkillTarget target, SkillTarget containTarget)
        {
            if (_memoryDbData.Get().SkillTargetMasterTable.TryFindByTarget(target.ToInt32(), out var skillTarget))
            {
                return skillTarget.Targets.Any(x => x == containTarget.ToInt32());
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
            }
        }

        public IEnumerable<SkillType> FindsSkillType(SkillTarget target)
        {
            var targets = _memoryDbData.Get().SkillTargetMasterTable.All
                .Where(x => x.Targets.Contains(target.ToInt32()))
                .Select(x => x.Target);

            return _memoryDbData.Get().SkillBaseMasterTable.All
                .Where(x => targets.Contains(x.Target))
                .Select(x => x.Type.ToSkillType());
        }
    }
}