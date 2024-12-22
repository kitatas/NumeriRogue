using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HandUseCase
    {
        private readonly DeckEntity _deckEntity;
        private HandEntity _playerHandEntity;
        private HandEntity _enemyHandEntity;

        public HandUseCase(DeckEntity deckEntity)
        {
            _deckEntity = deckEntity;
        }

        public void SetUp()
        {
            _playerHandEntity = new HandEntity();
            _enemyHandEntity = new HandEntity();

            for (int i = 0; i < HandConfig.MAX_NUM; i++)
            {
                _playerHandEntity.Add(_deckEntity.Draw());
                _enemyHandEntity.Add(_deckEntity.Draw());
            }
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
    }
}