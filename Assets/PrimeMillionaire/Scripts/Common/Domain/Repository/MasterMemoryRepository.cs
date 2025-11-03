using Newtonsoft.Json;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class MasterMemoryRepository
    {
        private readonly MemoryData _memoryData;

        public MasterMemoryRepository(MemoryData memoryData)
        {
            _memoryData = memoryData;
        }

        public void Build(MasterVO master)
        {
            var immutableBuilder = _memoryData.Get().ToImmutableBuilder();
            foreach (var (key, values) in master.data)
            {
                switch (key)
                {
                    case PlayFabConfig.MASTER_APP_VERSION_KEY:
                        immutableBuilder.ReplaceAll(JsonConvert.DeserializeObject<AppVersionMaster[]>(values));
                        break;
                }
            }

            _memoryData.Set(immutableBuilder.Build());
        }
    }
}