using FastEnumUtility;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class BonusRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public BonusRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public BonusTargetVO Find(BonusType type)
        {
            if (_memoryDatabase.BonusTargetMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
            }
        }
    }
}