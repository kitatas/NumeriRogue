using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DropUseCase
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly TurnEntity _turnEntity;
        private readonly DropRepository _dropRepository;
        private readonly LevelRepository _levelRepository;

        public DropUseCase(EnemyCountEntity enemyCountEntity, TurnEntity turnEntity, DropRepository dropRepository,
            LevelRepository levelRepository)
        {
            _enemyCountEntity = enemyCountEntity;
            _turnEntity = turnEntity;
            _dropRepository = dropRepository;
            _levelRepository = levelRepository;
        }

        public int GetDropDollar()
        {
            var baseDropDollar = 80;
            var level = _levelRepository.FindClosest(_enemyCountEntity.currentValue);
            var wrapTurn = Mathf.Min(5, _turnEntity.currentValue);
            return Mathf.CeilToInt(baseDropDollar * _dropRepository.FindDropRate(wrapTurn) * level.rate);
        }
    }
}