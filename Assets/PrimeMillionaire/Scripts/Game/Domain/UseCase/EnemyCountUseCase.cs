using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class EnemyCountUseCase
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly int _maxEnemyCount;

        public EnemyCountUseCase(EnemyCountEntity enemyCountEntity, PlayerCharacterEntity playerCharacterEntity,
            CharacterStageRepository characterStageRepository)
        {
            _enemyCountEntity = enemyCountEntity;
            _maxEnemyCount = characterStageRepository.GetStage(playerCharacterEntity.type).maxEnemyCount;
        }

        public void Increment()
        {
            _enemyCountEntity.Add(1);
        }

        public bool IsClear()
        {
            return _enemyCountEntity.currentValue == _maxEnemyCount;
        }
    }
}