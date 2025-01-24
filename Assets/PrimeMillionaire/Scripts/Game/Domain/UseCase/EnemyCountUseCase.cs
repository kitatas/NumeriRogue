using System;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class EnemyCountUseCase : IDisposable
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly ReactiveProperty<int> _enemyCount;

        public EnemyCountUseCase(EnemyCountEntity enemyCountEntity)
        {
            _enemyCountEntity = enemyCountEntity;
            _enemyCount = new ReactiveProperty<int>(_enemyCountEntity.currentValue);
        }

        public Observable<int> enemyCount => _enemyCount.Where(x => x != 0);

        public void Increment()
        {
            _enemyCountEntity.Add(1);
            _enemyCount.Value = _enemyCountEntity.currentValue;
        }

        public void Reset()
        {
            _enemyCountEntity.Reset();
            _enemyCount.Value = _enemyCountEntity.currentValue;
        }

        public void Dispose()
        {
            _enemyCount?.Dispose();
        }
    }
}