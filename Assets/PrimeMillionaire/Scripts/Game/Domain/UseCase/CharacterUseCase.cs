using PrimeMillionaire.Game.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class CharacterUseCase
    {
        private readonly CharacterRepository _characterRepository;

        public CharacterUseCase(CharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public CharacterVO GetPlayerCharacter()
        {
            // TODO: CharacterTypeのベタ書き
            return _characterRepository.Find(CharacterType.Andromeda);
        }

        public CharacterVO GetEnemyCharacter()
        {
            // TODO: CharacterTypeのベタ書き
            return _characterRepository.Find(CharacterType.Borealjuggernaut);
        }
    }
}