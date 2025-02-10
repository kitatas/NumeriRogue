using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class LevelRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public LevelRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public LevelVO FindClosest(int level)
        {
            return _memoryDatabase.LevelMasterTable.FindClosestByLevel(level).ToVO();
        }
    }
}