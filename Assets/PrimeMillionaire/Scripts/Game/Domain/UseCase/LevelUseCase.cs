using System;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class LevelUseCase : IDisposable
    {
        private readonly LevelEntity _levelEntity;
        private readonly ReactiveProperty<int> _level;

        public LevelUseCase(LevelEntity levelEntity)
        {
            _levelEntity = levelEntity;
            _level = new ReactiveProperty<int>(_levelEntity.currentValue);
        }

        public Observable<int> level => _level.Where(x => x != 0);

        public void Update()
        {
            _level.Value = _levelEntity.currentValue;
        }

        public void Increment()
        {
            _levelEntity.Add(1);
            Update();
        }

        public void Reset()
        {
            _levelEntity.Reset();
            Update();
        }

        public bool IsClear()
        {
            return _levelEntity.currentValue == LevelConfig.CLEAR_LEVEL;
        }

        public void Dispose()
        {
            _level?.Dispose();
        }
    }
}