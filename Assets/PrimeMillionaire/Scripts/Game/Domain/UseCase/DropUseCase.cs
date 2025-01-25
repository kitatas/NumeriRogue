using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DropUseCase
    {
        private readonly TurnEntity _turnEntity;
        private readonly DropRepository _dropRepository;

        public DropUseCase(TurnEntity turnEntity, DropRepository dropRepository)
        {
            _turnEntity = turnEntity;
            _dropRepository = dropRepository;
        }

        public int GetDropDollar()
        {
            // TODO: 敵の強さから獲得可能な$を取得する
            var baseDropDollar = 500;
            var wrapTurn = Mathf.Min(5, _turnEntity.currentValue);
            return Mathf.CeilToInt(baseDropDollar * _dropRepository.FindDropRate(wrapTurn));
        }
    }
}