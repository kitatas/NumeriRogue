using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class FailState : BaseState
    {
        private readonly FailView _failView;

        public FailState(FailView failView)
        {
            _failView = failView;
        }

        public override GameState state => GameState.Fail;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _failView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _failView.FadeIn(0.25f).WithCancellation(token);

            await UniTaskHelper.DelayAsync(1.0f, token);

            return GameState.Load;
        }
    }
}