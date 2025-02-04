using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class CharacterUseCase
    {
        private readonly CharacterRepository _characterRepository;

        public CharacterUseCase(CharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async UniTask RenderPlayableCharacterAsync(CancellationToken token)
        {
            var characters = _characterRepository.GetAll();
            foreach (var character in characters)
            {
                await Router.Default.PublishAsync(character, token).AsUniTask();
            }
        }
    }
}