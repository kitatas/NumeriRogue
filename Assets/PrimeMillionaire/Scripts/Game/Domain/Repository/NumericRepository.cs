using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class NumericRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public NumericRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public bool IsAny(int value)
        {
            return _memoryDbData.Get().NumericMasterTable.FindByValue(value).Any();
        }

        public BonusVO Find(BonusType type)
        {
            if (_memoryDbData.Get().NumericBonusMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            }
        }

        public BonusTargetVO FindTarget(BonusType type)
        {
            if (_memoryDbData.Get().NumericBonusMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToTargetVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            }
        }

        public IEnumerable<BonusVO> Finds(int value)
        {
            return _memoryDbData.Get().NumericMasterTable.FindByValue(value)
                .Select(x => Find(x.Bonus.ToBonusType()))
                .OrderBy(x => x.type);
        }
    }
}