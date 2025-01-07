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

        public (CharacterVO player, CharacterVO enemy) GetBattleCharacters()
        {
            // TODO: CharacterTypeのベタ書き
            return (
                player: _characterRepository.Find(CharacterType.Andromeda),
                enemy: _characterRepository.Find(CharacterType.Borealjuggernaut)
            );
        }
    }
}