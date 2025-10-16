using System.Threading;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class PlayFabRepository
    {
        public PlayFabRepository()
        {
            PlayFabSettings.staticSettings.TitleId = PlayFabConfig.TITLE_ID;
        }

        public async UniTask<UserVO> LoginAsync(string uid, CancellationToken token)
        {
            var completionSource = new UniTaskCompletionSource<LoginResult>();
            var request = new LoginWithCustomIDRequest
            {
                CustomId = uid,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserData = true,
                    GetPlayerProfile = true,
                },
            };

            PlayFabClientAPI.LoginWithCustomID(
                request,
                result => completionSource.TrySetResult(result),
                error => completionSource.TrySetException(new RebootExceptionVO(error.ErrorMessage))
            );

            var response = await completionSource.Task.AttachExternalCancellation(token);
            var user = new UserDTO(response);
            return user.ToVO();
        }
    }
}