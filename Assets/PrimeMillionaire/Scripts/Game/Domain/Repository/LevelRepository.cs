using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class LevelRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public LevelRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public LevelVO FindClosest(int level)
        {
            return _memoryDbData.Get().LevelMasterTable.FindClosestByLevel(level).ToVO();
        }
    }
}