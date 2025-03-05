using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoadState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;

        public LoadState(SceneUseCase sceneUseCase)
        {
            _sceneUseCase = sceneUseCase;
        }

        public override BootState state => BootState.Load;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _sceneUseCase.Load(SceneName.Top, LoadType.Fade);
            await UniTask.Yield(token);
            return BootState.None;
        }
    }
}