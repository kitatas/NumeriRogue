using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class LoadState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;

        public LoadState(SceneUseCase sceneUseCase)
        {
            _sceneUseCase = sceneUseCase;
        }

        public override GameState state => GameState.Load;

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTaskHelper.DelayAsync(0.5f, token);
            _sceneUseCase.Load(SceneName.Top, LoadType.Fade);
            return GameState.None;
        }
    }
}