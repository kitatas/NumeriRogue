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

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, EnemyCharacterEntity enemyCharacterEntity,
            CharacterRepository characterRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _characterRepository = characterRepository;
        }

        public CharacterVO GetCharacter(Side side)
        {
            return _characterRepository.Find(GetCharacterId(side));
        }

        public CharacterVO LotEnemyCharacter()
        {
            var character = _characterRepository.FindOther(GetCharacterId(Side.Player));
            _enemyCharacterEntity.SetType(character.parameter.id);
            return character;
        }

        private int GetCharacterId(Side side)
        {
            return side switch
            {
                Side.Player => _playerCharacterEntity.id,
                Side.Enemy => _enemyCharacterEntity.id,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }
    }
}