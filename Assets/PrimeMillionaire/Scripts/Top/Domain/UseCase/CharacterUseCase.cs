using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Top.Domain.UseCase
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

        public async UniTask RenderPlayableCharacterAsync(CancellationToken token)
        {
            var characters = _characterRepository.GetAll();
            foreach (var character in characters)
            {
                await Router.Default.PublishAsync(character, token).AsUniTask();
            }

            Order(characters[0].type);
        }

        public void Order(CharacterType type)
        {
            _playerCharacterEntity.SetType(type);

        }
    }
}