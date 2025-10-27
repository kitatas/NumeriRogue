using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class CheckState : BaseState
    {
        private readonly AppVersionUseCase _appVersionUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ModalUseCase _modalUseCase;

        public CheckState(AppVersionUseCase appVersionUseCase, LoadingUseCase loadingUseCase, ModalUseCase modalUseCase)
        {
            _appVersionUseCase = appVersionUseCase;
            _loadingUseCase = loadingUseCase;
            _modalUseCase = modalUseCase;
        }

        public override BootState state => BootState.Check;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _loadingUseCase.Set(true);

            var isForceUpdate = await _appVersionUseCase.IsForceUpdateAsync(token);

            _loadingUseCase.Set(false);

            if (isForceUpdate)
            {
                await _modalUseCase.ShowAsync(ModalType.Update, token);
                return BootState.None;
            }

            return BootState.Start;
        }
    }
}