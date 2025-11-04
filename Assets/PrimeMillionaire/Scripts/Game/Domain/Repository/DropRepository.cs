using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class DropRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public DropRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public DropRateVO FindClosest(int turn)
        {
            return _memoryDbData.Get().DropRateMasterTable.FindClosestByTurn(turn).ToVO();
        }
    }
}