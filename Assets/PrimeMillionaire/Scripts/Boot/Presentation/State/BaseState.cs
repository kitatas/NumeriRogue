using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public abstract class BaseState
    {
        public abstract BootState state { get; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async UniTask InitAsync(CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async UniTask<BootState> TickAsync(CancellationToken token)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return BootState.None;
        }
    }
}