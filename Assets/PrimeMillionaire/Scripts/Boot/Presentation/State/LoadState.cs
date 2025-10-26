using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoadState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ISoundUseCase _soundUseCase;

        public LoadState(LoadingUseCase loadingUseCase, ISoundUseCase soundUseCase)
        {
            _loadingUseCase = loadingUseCase;
            _soundUseCase = soundUseCase;
        }

        public override BootState state => BootState.Load;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            _loadingUseCase.Set(false);

            _soundUseCase.Play(Bgm.Menu);

            return BootState.Login;
        }
    }
}