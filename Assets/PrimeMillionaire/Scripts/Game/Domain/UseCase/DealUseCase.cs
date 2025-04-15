using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DealUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly DeckEntity _deckEntity;
        private readonly PlayerHandEntity _playerHandEntity;
        private readonly EnemyHandEntity _enemyHandEntity;
        private readonly CharacterStageRepository _characterStageRepository;

        public DealUseCase(PlayerCharacterEntity playerCharacterEntity, DeckEntity deckEntity,
            PlayerHandEntity playerHandEntity, EnemyHandEntity enemyHandEntity,
            CharacterStageRepository characterStageRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _deckEntity = deckEntity;
            _playerHandEntity = playerHandEntity;
            _enemyHandEntity = enemyHandEntity;
            _characterStageRepository = characterStageRepository;
        }

        public void Init()
        {
            var deck = _characterStageRepository.GetCards(_playerCharacterEntity.type);
            _deckEntity.Init(deck);
        }

        public void SetUp()
        {
            _deckEntity.Refresh();
            SetUpHands();
        }

        public void SetUpHands()
        {
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