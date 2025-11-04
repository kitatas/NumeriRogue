using Newtonsoft.Json;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class MasterMemoryRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public MasterMemoryRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public void Build(MasterVO master)
        {
            var immutableBuilder = _memoryDbData.Get().ToImmutableBuilder();
            foreach (var (key, values) in master.data)
            {
                switch (key)
                {
                    case PlayFabConfig.MASTER_APP_VERSION_KEY:
                        immutableBuilder.ReplaceAll(JsonConvert.DeserializeObject<AppVersionMaster[]>(values));
                        break;
                }
            }

            _memoryDbData.Set(immutableBuilder.Build());
        }
    }
}