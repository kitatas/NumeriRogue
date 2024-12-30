using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<HandVO> GetPlayerHands()
        {
            return _playerHandEntity.hands
                .Select((v, i) => new HandVO(i, _deckEntity.GetCard(v)));
        }

        public IEnumerable<HandVO> GetEnemyHands()
        {
            return _enemyHandEntity.hands
                .Select((v, i) => new HandVO(i, _deckEntity.GetCard(v)));
        }

        public CardVO GetPlayerCard(int index)
        {
            return _deckEntity.GetCard(_playerHandEntity.hands[index]);
        }
    }
}