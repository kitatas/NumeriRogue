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
            return _memoryDatabase.SkillMasterTable.FindByLevel(level)
                .OrderBy(_ => random.Next())
                .Take(SkillConfig.LOT_NUM)
                .Select(x => x.ToVO())
                .ToList();
        }
    }
}