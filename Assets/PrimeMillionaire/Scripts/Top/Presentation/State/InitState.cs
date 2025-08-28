using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly SoundUseCase _soundUseCase;

        public InitState(LoadingUseCase loadingUseCase, SoundUseCase soundUseCase)
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
            await UniTask.Yield(token);
            _loadingUseCase.Set(false);

            _soundUseCase.Play(Bgm.Menu);

            return TopState.Order;
        }
    }
}