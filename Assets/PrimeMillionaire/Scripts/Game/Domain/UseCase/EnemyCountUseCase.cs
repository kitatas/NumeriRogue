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
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly CharacterStageRepository _characterStageRepository;
        private readonly ReactiveProperty<int> _enemyCount;
        private int? _maxEnemyCount;

        public EnemyCountUseCase(EnemyCountEntity enemyCountEntity, PlayerCharacterEntity playerCharacterEntity,
            CharacterStageRepository characterStageRepository)
        {
            _enemyCountEntity = enemyCountEntity;
            _playerCharacterEntity = playerCharacterEntity;
            _characterStageRepository = characterStageRepository;
            _enemyCount = new ReactiveProperty<int>(_enemyCountEntity.currentValue);
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
            _maxEnemyCount ??= _characterStageRepository.GetStage(_playerCharacterEntity.type).maxEnemyCount;
            return _enemyCountEntity.currentValue == _maxEnemyCount;
        }

        public void Dispose()
        {
            _enemyCount?.Dispose();
        }
    }
}