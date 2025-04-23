using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;

        public InitState(LoadingUseCase loadingUseCase)
        {
            _loadingUseCase = loadingUseCase;
        }

        public override TopState state => TopState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            _loadingUseCase.Set(false);

            return TopState.Order;
        }
    }
}