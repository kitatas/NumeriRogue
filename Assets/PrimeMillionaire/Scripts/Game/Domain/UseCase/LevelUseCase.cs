using System;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class LevelUseCase : IDisposable
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly LevelEntity _levelEntity;
        private readonly ReactiveProperty<int> _level;

        public LevelUseCase(EnemyCountEntity enemyCountEntity, LevelEntity levelEntity)
        {
            _enemyCountEntity = enemyCountEntity;
            _levelEntity = levelEntity;
            _level = new ReactiveProperty<int>(_levelEntity.currentValue);
        }

        public Observable<int> level => _level.Where(x => x != 0);

        public void Update()
        {
            _level.Value = _levelEntity.currentValue;
        }

        public void Lot()
        {
            _levelEntity.Set(_enemyCountEntity.currentValue);
            Update();
        }

        public void Reset()
        {
            _levelEntity.Reset();
            Update();
        }

        public void Dispose()
        {
            _level?.Dispose();
        }
    }
}