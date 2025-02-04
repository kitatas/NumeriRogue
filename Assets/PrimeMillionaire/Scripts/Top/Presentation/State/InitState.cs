using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Top.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;

        public InitState(CharacterUseCase characterUseCase)
        {
            _characterUseCase = characterUseCase;
        }

        public override TopState state => TopState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await _characterUseCase.RenderPlayableCharacterAsync(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            return TopState.None;
        }
    }
}