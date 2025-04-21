using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {

        public override TopState state => TopState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            return TopState.Order;
        }
    }
}