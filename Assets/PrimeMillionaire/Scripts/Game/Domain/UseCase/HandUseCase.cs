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

        public List<HandVO> GetPlayerHands()
        {
            return _playerHandEntity.hands
                .Select((v, i) => new HandVO(i, _deckEntity.GetCard(v)))
                .ToList();
        }

        public List<HandVO> GetEnemyHands()
        {
            return _enemyHandEntity.hands
                .Select((v, i) => new HandVO(i, _deckEntity.GetCard(v)))
                .ToList();
        }

        public CardVO GetPlayerCard(int index)
        {
            return _deckEntity.GetCard(_playerHandEntity.hands[index]);
        }

        public CardVO GetEnemyCard(int index)
        {
            return _deckEntity.GetCard(_enemyHandEntity.hands[index]);
        }

        public void RemovePlayerCards(IEnumerable<int> index)
        {
            foreach (var i in index)
            {
                _playerHandEntity.Remove(i);
            }
        }

        public void RemoveEnemyCards(IEnumerable<int> index)
        {
            foreach (var i in index)
            {
                _enemyHandEntity.Remove(i);
            }
        }

        public bool IsPlayerHandsEmpty()
        {
            return _playerHandEntity.hands.Count == 0;
        }
    }
}