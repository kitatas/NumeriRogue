using System.Collections.Generic;
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
            var user = new UserDTO(uid, response);
            return user.ToVO();
        }

        public async UniTask UpdateUserProgressAsync(ProgressVO progress, CancellationToken token)
        {
            var completionSource = new UniTaskCompletionSource<UpdateUserDataResult>();
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { PlayFabConfig.USER_PROGRESS_KEY, new ProgressDTO(progress).ToJson() },
                },
            };

            PlayFabClientAPI.UpdateUserData(
                request,
                result => completionSource.TrySetResult(result),
                error => completionSource.TrySetException(new RebootExceptionVO(error.ErrorMessage))
            );

            await completionSource.Task.AttachExternalCancellation(token);
        }

        public async UniTask<MasterVO> GetMasterAsync(CancellationToken token)
        {
            var completionSource = new UniTaskCompletionSource<GetTitleDataResult>();
            var request = new GetTitleDataRequest();

            PlayFabClientAPI.GetTitleData(
                request,
                result => completionSource.TrySetResult(result),
                error => completionSource.TrySetException(new RebootExceptionVO(error.ErrorMessage))
            );

            var response = await completionSource.Task.AttachExternalCancellation(token);
            var master = new MasterDTO(response);
            return master.ToVO();
        }
    }
}