using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class ClearState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly ProgressUseCase _progressUseCase;
        private readonly ClearView _clearView;

        public ClearState(InterruptUseCase interruptUseCase, ProgressUseCase progressUseCase, ClearView clearView)
        {
            _interruptUseCase = interruptUseCase;
            _progressUseCase = progressUseCase;
            _clearView = clearView;
        }

        public override GameState state => GameState.Clear;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _clearView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _interruptUseCase.Delete();
            _progressUseCase.UpdateProgress();
            await _clearView.FadeIn(0.25f).WithCancellation(token);

            await UniTaskHelper.DelayAsync(1.0f, token);

            return GameState.Load;
        }
    }
}