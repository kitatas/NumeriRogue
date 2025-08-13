using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HandUseCase
    {
        private readonly DeckEntity _deckEntity;
        private readonly PlayerHandEntity _playerHandEntity;
        private readonly EnemyHandEntity _enemyHandEntity;

        public HandUseCase(DeckEntity deckEntity, PlayerHandEntity playerHandEntity, EnemyHandEntity enemyHandEntity)
        {
            _deckEntity = deckEntity;
            _playerHandEntity = playerHandEntity;
            _enemyHandEntity = enemyHandEntity;
        }

        public List<HandVO> GetHands(Side side)
        {
            return GetHandEntity(side).hands
                .Select((v, i) => new HandVO(i, _deckEntity.GetCard(v)))
                .ToList();
        }

        public CardVO GetCard(Side side, int index)
        {
            return _deckEntity.GetCard(GetHandEntity(side).hands[index]);
        }

        public void RemoveCards(Side side, IEnumerable<int> index)
        {
            foreach (var i in index)
            {
                GetHandEntity(side).Remove(i);
            }
        }

        private BaseHandEntity GetHandEntity(Side side)
        {
            return side switch
            {
                Side.Player => _playerHandEntity,
                Side.Enemy => _enemyHandEntity,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public bool IsPlayerHandsEmpty()
        {
            return _playerHandEntity.hands.Count == 0;
        }
    }
}