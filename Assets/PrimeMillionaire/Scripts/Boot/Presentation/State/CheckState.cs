using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class CheckState : BaseState
    {
        private readonly AppVersionUseCase _appVersionUseCase;
        private readonly ModalUseCase _modalUseCase;

        public CheckState(AppVersionUseCase appVersionUseCase, ModalUseCase modalUseCase)
        {
            _appVersionUseCase = appVersionUseCase;
            _modalUseCase = modalUseCase;
        }

        public override BootState state => BootState.Check;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            var isForceUpdate = await _appVersionUseCase.IsForceUpdateAsync(token);
            if (isForceUpdate)
            {
                await _modalUseCase.ShowAsync(ModalType.Update, token);
                return BootState.None;
            }

            return BootState.Start;
        }
    }
}