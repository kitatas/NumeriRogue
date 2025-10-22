using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab.ClientModels;

namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class UserDTO
    {
        private readonly Dictionary<string, UserDataRecord> _data;
        public readonly string uid;
        public readonly bool isNewly;

        public UserDTO(string uid, LoginResult loginResult)
        {
            var payload = loginResult.InfoResultPayload;
            if (payload == null) throw new QuitExceptionVO(ExceptionConfig.FAILED_FETCH_PAYLOAD);

            _data = payload.UserData;
            if (_data == null) throw new QuitExceptionVO(ExceptionConfig.FAILED_FETCH_USER_DATA);

            this.uid = uid;
            this.isNewly = loginResult.NewlyCreated;
        }

        public ProgressDTO progress => _data.TryGetValue(PlayFabConfig.USER_PROGRESS_KEY, out var record)
            ? JsonConvert.DeserializeObject<ProgressDTO>(record.Value)
            : new ProgressDTO();

        public UserVO ToVO() => new(uid, isNewly, progress.ToVO());
    }
}