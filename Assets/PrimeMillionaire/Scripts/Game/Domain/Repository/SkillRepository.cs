using System.Collections.Generic;
using System.Linq;
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
    }
}