using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class AppVersionUseCase
    {
        private readonly PlayFabRepository _playFabRepository;

        public AppVersionUseCase(PlayFabRepository playFabRepository)
        {
            _playFabRepository = playFabRepository;
        }

        public async UniTask<bool> IsForceUpdateAsync(CancellationToken token)
        {
            var master = await _playFabRepository.GetMasterAsync(token);
            return master.appVersion.isForceUpdate;
        }
    }
}