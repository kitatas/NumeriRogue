using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;

namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class MasterDTO
    {
        private readonly Dictionary<string, string> _data;

        public MasterDTO(GetTitleDataResult getTitleDataResult)
        {
            _data = getTitleDataResult.Data;
            if (_data == null) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_DATA);
        }

        public MasterVO ToVO() => new(
            DeserializeMaster<AppVersionDTO>(PlayFabConfig.MASTER_APP_VERSION_KEY).ToVO()
        );

        private T DeserializeMaster<T>(string key)
        {
            return _data.TryGetValue(key, out var json)
                ? JsonConvert.DeserializeObject<T>(json)
                : throw new QuitExceptionVO(ExceptionConfig.FAILED_DESERIALIZE_MASTER);
        }
    }
}