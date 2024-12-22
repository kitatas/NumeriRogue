using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DealUseCase
    {
        private readonly DeckEntity _deckEntity;
        private readonly PlayerHandEntity _playerHandEntity;
        private readonly EnemyHandEntity _enemyHandEntity;
        private readonly CardRepository _cardRepository;

        public DealUseCase(DeckEntity deckEntity, PlayerHandEntity playerHandEntity, EnemyHandEntity enemyHandEntity,
            CardRepository cardRepository)
        {
            _deckEntity = deckEntity;
            _playerHandEntity = playerHandEntity;
            _enemyHandEntity = enemyHandEntity;
            _cardRepository = cardRepository;
        }

        public void Init()
        {
            _deckEntity.Init(_cardRepository.GetAll());
        }

        public void SetUp()
        {
            _deckEntity.Refresh();
            _playerHandEntity.Clear();
            _enemyHandEntity.Clear();

            for (int i = 0; i < HandConfig.MAX_NUM; i++)
            {
                DealToPlayer();
                DealToEnemy();
            }
        }

        public void DealToPlayer()
        {
            _playerHandEntity.Add(_deckEntity.Draw());
        }

        public void DealToEnemy()
        {
            _enemyHandEntity.Add(_deckEntity.Draw());
        }
    }
}