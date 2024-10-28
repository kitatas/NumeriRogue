using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly DeckUseCase _deckUseCase;

        public InitState(DeckUseCase deckUseCase)
        {
            _deckUseCase = deckUseCase;
        }

        public override GameState state => GameState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _deckUseCase.Build();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            return GameState.None;
        }
    }
}