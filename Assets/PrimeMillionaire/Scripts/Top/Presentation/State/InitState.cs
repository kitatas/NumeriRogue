using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ISoundUseCase _soundUseCase;

        public InitState(LoadingUseCase loadingUseCase, ISoundUseCase soundUseCase)
        {
            _loadingUseCase = loadingUseCase;
            _soundUseCase = soundUseCase;
        }

        public override TopState state => TopState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTaskHelper.DelayAsync(2.5f, token);
            _loadingUseCase.Set(false);

            _soundUseCase.Play(Bgm.Menu);

            return TopState.Order;
        }
    }
}