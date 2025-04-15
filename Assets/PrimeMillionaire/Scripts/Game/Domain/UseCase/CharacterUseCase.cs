using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class CharacterUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterStageRepository _characterStageRepository;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, EnemyCharacterEntity enemyCharacterEntity,
            CharacterRepository characterRepository, CharacterStageRepository characterStageRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _characterRepository = characterRepository;
            _characterStageRepository = characterStageRepository;
        }

        public CharacterVO GetPlayerCharacter()
        {
            return _characterRepository.Find(_playerCharacterEntity.type);
        }

        public StageVO GetStage()
        {
            return _characterStageRepository.GetStage(_playerCharacterEntity.type);
        }

        public CharacterVO GetEnemyCharacter()
        {
            return _characterRepository.Find(_enemyCharacterEntity.type);
        }

        public CharacterVO LotEnemyCharacter()
        {
            var character = _characterRepository.FindOther(_playerCharacterEntity.type);
            _enemyCharacterEntity.SetType(character.type);
            return character;
        }
    }
}