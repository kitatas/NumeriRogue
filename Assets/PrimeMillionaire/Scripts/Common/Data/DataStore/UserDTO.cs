using PlayFab.ClientModels;

namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class UserDTO
    {
        public readonly string uid;
        public readonly bool isNewly;

        public UserDTO(LoginResult loginResult)
        {
            var payload = loginResult.InfoResultPayload;
            if (payload == null) throw new QuitExceptionVO(ExceptionConfig.FAILED_FETCH_PAYLOAD);

            var userData = payload.UserData;
            if (userData == null) throw new QuitExceptionVO(ExceptionConfig.FAILED_FETCH_USER_DATA);

            var playerProfile = payload.PlayerProfile;
            uid = playerProfile == null ? "" : playerProfile.PlayerId;
            isNewly = loginResult.NewlyCreated;
        }

        public UserVO ToVO() => new(uid, isNewly);
    }
}