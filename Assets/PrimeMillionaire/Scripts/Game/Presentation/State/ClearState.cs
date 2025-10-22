using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class ClearState : BaseState
    {
        private readonly FinishUseCase _finishUseCase;
        private readonly InterruptUseCase _interruptUseCase;
        private readonly ProgressUseCase _progressUseCase;

        public ClearState(FinishUseCase finishUseCase, InterruptUseCase interruptUseCase,
            ProgressUseCase progressUseCase)
        {
            _finishUseCase = finishUseCase;
            _interruptUseCase = interruptUseCase;
            _progressUseCase = progressUseCase;
        }

        public override GameState state => GameState.Clear;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _interruptUseCase.Delete();
            await _progressUseCase.UpdateProgressAsync(ProgressStatus.Clear, token);
            await _finishUseCase.FadeInViewAsync(FinishType.Clear, token);

            return GameState.Load;
        }
    }
}