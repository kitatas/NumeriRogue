using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class ClearState : BaseState
    {
        private readonly FinishUseCase _finishUseCase;
        private readonly InterruptUseCase _interruptUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ProgressUseCase _progressUseCase;

        public ClearState(FinishUseCase finishUseCase, InterruptUseCase interruptUseCase,
            LoadingUseCase loadingUseCase, ProgressUseCase progressUseCase)
        {
            _finishUseCase = finishUseCase;
            _interruptUseCase = interruptUseCase;
            _loadingUseCase = loadingUseCase;
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
            await _finishUseCase.FadeInViewAsync(FinishType.Clear, token);

            _loadingUseCase.Set(true);

            await (
                _progressUseCase.UpdateProgressAsync(ProgressStatus.Clear, token),
                UniTaskHelper.DelayAsync(1.0f, token)
            );

            _loadingUseCase.Set(false);

            return GameState.Load;
        }
    }
}