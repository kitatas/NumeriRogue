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

        public CharacterVO GetCharacter(Side side)
        {
            return _characterRepository.Find(GetCharacterType(side));
        }

        public StageVO GetStage()
        {
            return _characterStageRepository.GetStage(GetCharacterType(Side.Player));
        }

        public CharacterVO LotEnemyCharacter()
        {
            var character = _characterRepository.FindOther(GetCharacterType(Side.Player));
            _enemyCharacterEntity.SetType(character.type);
            return character;
        }

        private CharacterType GetCharacterType(Side side)
        {
            return side switch
            {
                Side.Player => _playerCharacterEntity.type,
                Side.Enemy => _enemyCharacterEntity.type,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }
    }
}