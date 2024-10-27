using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Game.Presentation.State
{
    public abstract class BaseState
    {
        public abstract GameState state { get; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async UniTask InitAsync(CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async UniTask<GameState> TickAsync(CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return GameState.None;
        }
    }
}