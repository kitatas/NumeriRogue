using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class CheckState : BaseState
    {
        private readonly AppVersionUseCase _appVersionUseCase;

        public CheckState(AppVersionUseCase appVersionUseCase)
        {
            _appVersionUseCase = appVersionUseCase;
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
                // TODO: 強制アプデのモーダル表示
                return BootState.None;
            }

            return BootState.Start;
        }
    }
}