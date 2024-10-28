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
            _deckEntity.SetUp();

            _playerHandEntity = new HandEntity();
            _enemyHandEntity = new HandEntity();

            for (int i = 0; i < HandConfig.MAX_NUM; i++)
            {
                _playerHandEntity.Set(i, _deckEntity.Draw());
                _enemyHandEntity.Set(i, _deckEntity.Draw());
            }
        }
    }
}