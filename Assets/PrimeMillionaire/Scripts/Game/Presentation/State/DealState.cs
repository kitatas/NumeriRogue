using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class DealState : BaseState
    {
        private readonly DealUseCase _dealUseCase;

        public DealState(DealUseCase dealUseCase)
        {
            _dealUseCase = dealUseCase;
        }

        public override GameState state => GameState.Deal;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _dealUseCase.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            return GameState.None;
        }
    }
}