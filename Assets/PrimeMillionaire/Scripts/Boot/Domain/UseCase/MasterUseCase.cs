using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.Repository;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class MasterUseCase
    {
        private readonly AppVersionRepository _appVersionRepository;
        private readonly MasterMemoryRepository _memoryRepository;
        private readonly PlayFabRepository _playFabRepository;

        public MasterUseCase(AppVersionRepository appVersionRepository, MasterMemoryRepository memoryRepository,
            PlayFabRepository playFabRepository)
        {
            _appVersionRepository = appVersionRepository;
            _memoryRepository = memoryRepository;
            _playFabRepository = playFabRepository;
        }

        public async UniTask FetchAndBuildAsync(CancellationToken token)
        {
            var master = await _playFabRepository.GetMasterAsync(token);
            _memoryRepository.Build(master);
        }

        public bool IsForceUpdate()
        {
            return _appVersionRepository.Get().isForceUpdate;
        }
    }
}