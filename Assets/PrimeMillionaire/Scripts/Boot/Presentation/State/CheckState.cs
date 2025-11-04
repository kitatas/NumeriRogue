using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class CheckState : BaseState
    {
        private readonly MasterUseCase _masterUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ModalUseCase _modalUseCase;

        public CheckState(MasterUseCase masterUseCase, LoadingUseCase loadingUseCase, ModalUseCase modalUseCase)
        {
            _masterUseCase = masterUseCase;
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

            await _masterUseCase.FetchAndBuildAsync(token);

            _loadingUseCase.Set(false);

            if (_masterUseCase.IsForceUpdate())
            {
                await _modalUseCase.ShowAsync(ModalType.Update, token);
                return BootState.None;
            }

            return BootState.Start;
        }
    }
}