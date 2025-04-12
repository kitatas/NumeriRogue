using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class PrimeNumberRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public PrimeNumberRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public bool IsExist(int value)
        {
            return _memoryDatabase.PrimeNumberMasterTable.TryFindByValue(value, out _);
        }
    }
}