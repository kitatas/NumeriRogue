using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DropUseCase
    {
        private readonly LevelEntity _levelEntity;
        private readonly TurnEntity _turnEntity;
        private readonly DropRepository _dropRepository;
        private readonly LevelRepository _levelRepository;

        public DropUseCase(LevelEntity levelEntity, TurnEntity turnEntity, DropRepository dropRepository,
            LevelRepository levelRepository)
        {
            _levelEntity = levelEntity;
            _turnEntity = turnEntity;
            _dropRepository = dropRepository;
            _levelRepository = levelRepository;
        }

        public int GetDropDollar()
        {
            var level = _levelRepository.FindClosest(_levelEntity.currentValue);
            var dropRate = _dropRepository.FindClosest(_turnEntity.currentValue);
            return Mathf.CeilToInt(DollarConfig.DROP_VALUE * level.rate * dropRate.rate);
        }
    }
}