using System;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class EnemyCountUseCase : IDisposable
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly ReactiveProperty<int> _enemyCount;
        private readonly int _maxEnemyCount;

        public EnemyCountUseCase(EnemyCountEntity enemyCountEntity, PlayerCharacterEntity playerCharacterEntity,
            CharacterStageRepository characterStageRepository)
        {
            _enemyCountEntity = enemyCountEntity;
            _enemyCount = new ReactiveProperty<int>(_enemyCountEntity.currentValue);
            _maxEnemyCount = characterStageRepository.GetStage(playerCharacterEntity.type).maxEnemyCount;
        }

        public Observable<int> enemyCount => _enemyCount;

        public void Update()
        {
            _enemyCount.Value = _enemyCountEntity.currentValue;
        }

        public void Increment()
        {
            _enemyCountEntity.Add(1);
            Update();
        }

        public bool IsClear()
        {
            return _enemyCountEntity.currentValue == _maxEnemyCount;
        }

        public void Dispose()
        {
            _enemyCount?.Dispose();
        }
    }
}