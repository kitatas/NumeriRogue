using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class FailState : BaseState
    {
        private readonly FinishUseCase _finishUseCase;
        private readonly InterruptUseCase _interruptUseCase;
        private readonly ProgressUseCase _progressUseCase;

        public FailState(FinishUseCase finishUseCase, InterruptUseCase interruptUseCase,
            ProgressUseCase progressUseCase)
        {
            _finishUseCase = finishUseCase;
            _interruptUseCase = interruptUseCase;
            _progressUseCase = progressUseCase;
        }

        public override GameState state => GameState.Fail;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _interruptUseCase.Delete();
            _progressUseCase.UpdateProgress(ProgressStatus.None);
            await _finishUseCase.FadeInViewAsync(FinishType.Fail, token);

            return GameState.Load;
        }
    }
}