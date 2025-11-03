using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Boot.Domain.Repository
{
    public sealed class AppVersionRepository
    {
        private readonly MemoryData _memoryData;

        public AppVersionRepository(MemoryData memoryData)
        {
            _memoryData = memoryData;
        }

        public AppVersionVO Get()
        {
            return _memoryData.Get().AppVersionMasterTable.All.First.ToVO();
        }
    }
}