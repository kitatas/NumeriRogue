using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class CharacterUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly CharacterRepository _characterRepository;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, CharacterRepository characterRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _characterRepository = characterRepository;
        }

        public CharacterVO GetPlayerCharacter()
        {
            return _characterRepository.Find(_playerCharacterEntity.type);
        }

        public CharacterVO GetEnemyCharacter()
        {
            // TODO: CharacterTypeのベタ書き
            return _characterRepository.Find(CharacterType.Borealjuggernaut);
        }
    }
}