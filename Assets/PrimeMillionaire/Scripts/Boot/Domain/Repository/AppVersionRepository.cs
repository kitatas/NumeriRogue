using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Boot.Domain.Repository
{
    public sealed class AppVersionRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public AppVersionRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public AppVersionVO Get()
        {
            return _memoryDbData.Get().AppVersionMasterTable.All.First.ToVO();
        }
    }
}