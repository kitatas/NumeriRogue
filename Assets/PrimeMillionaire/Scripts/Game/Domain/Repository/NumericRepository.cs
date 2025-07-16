using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Utility;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class NumericRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public NumericRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public bool IsAny(int value)
        {
            return _memoryDatabase.NumericMasterTable.FindByValue(value).Any();
        }

        public BonusVO Find(BonusType type)
        {
            if (_memoryDatabase.NumericBonusMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            }
        }

        public IEnumerable<BonusVO> Finds(int value)
        {
            return _memoryDatabase.NumericMasterTable.FindByValue(value)
                .Select(x => Find(x.Bonus.ToBonusType()))
                .OrderBy(x => x.type);
        }
    }
}