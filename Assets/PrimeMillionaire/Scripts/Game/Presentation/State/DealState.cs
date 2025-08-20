using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class DealState : BaseState
    {
        private readonly DealUseCase _dealUseCase;
        private readonly HandUseCase _handUseCase;
        private readonly InterruptUseCase _interruptUseCase;
        private readonly TurnUseCase _turnUseCase;

        public DealState(DealUseCase dealUseCase, HandUseCase handUseCase, InterruptUseCase interruptUseCase,
            TurnUseCase turnUseCase)
        {
            _dealUseCase = dealUseCase;
            _handUseCase = handUseCase;
            _interruptUseCase = interruptUseCase;
            _turnUseCase = turnUseCase;
        }

        public override GameState state => GameState.Deal;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _turnUseCase.Increment();
            _dealUseCase.SetUp();
            await _handUseCase.DealAsync(token);

            _interruptUseCase.Save();

            return GameState.Order;
        }
    }
}