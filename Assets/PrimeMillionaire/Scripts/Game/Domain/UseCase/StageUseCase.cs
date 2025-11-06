using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class StageUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly CharacterStageRepository _characterStageRepository;

        public StageUseCase(PlayerCharacterEntity playerCharacterEntity, CharacterStageRepository characterStageRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _characterStageRepository = characterStageRepository;
        }

        public async UniTask PublishStageAsync(CancellationToken token)
        {
            var stage = _characterStageRepository.GetStage(_playerCharacterEntity.id);
            await Router.Default.PublishAsync(stage, token);
        }
    }
}