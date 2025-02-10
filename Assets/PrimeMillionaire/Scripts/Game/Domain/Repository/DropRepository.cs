using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class DropRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public DropRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public DropRateVO FindClosest(int turn)
        {
            return _memoryDatabase.DropRateMasterTable.FindClosestByTurn(turn).ToVO();
        }
    }
}