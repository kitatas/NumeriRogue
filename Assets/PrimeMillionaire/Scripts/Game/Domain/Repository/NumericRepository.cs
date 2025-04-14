using System.Linq;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Game.Utility;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class NumericRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public NumericRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public bool IsExistPrimeNumber(int value) => IsExist(value, BonusType.PrimeNumber);
        public bool IsExistSameNumbers(int value) => IsExist(value, BonusType.SameNumbers);

        private bool IsExist(int value, BonusType type)
        {
            return _memoryDatabase.NumericMasterTable.FindByValue(value)
                .Any(x => x.Bonus.ToBonusType() == type);
        }
    }
}