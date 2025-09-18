using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class SkillRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public SkillRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public List<SkillVO> FindLotteryTargets(int level)
        {
            var random = new System.Random();
            return _memoryDatabase.SkillEffectMasterTable.FindClosestByLevel(level)
                .OrderBy(_ => random.Next())
                .Take(SkillConfig.LOT_NUM)
                .Select(skillEffect =>
                {
                    if (_memoryDatabase.SkillBaseMasterTable.TryFindByType(skillEffect.Type, out var skillBase))
                    {
                        return new SkillVO(skillBase.ToVO(), skillEffect.ToVO());
                    }
                    else
                    {
                        throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
                    }
                })
                .ToList();
        }

        public bool IsExistTarget(SkillTarget target, SkillTarget containTarget)
        {
            if (_memoryDatabase.SkillTargetMasterTable.TryFindByTarget(target.ToInt32(), out var skillTarget))
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
            var targets = _memoryDatabase.SkillTargetMasterTable.All
                .Where(x => x.Targets.Contains(target.ToInt32()))
                .Select(x => x.Target);

            return _memoryDatabase.SkillBaseMasterTable.All
                .Where(x => targets.Contains(x.Target))
                .Select(x => x.Type.ToSkillType());
        }
    }
}