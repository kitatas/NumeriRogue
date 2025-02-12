using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Presentation.View;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using R3;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoadState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly TitleView _titleView;

        public LoadState(SceneUseCase sceneUseCase, TitleView titleView)
        {
            _sceneUseCase = sceneUseCase;
            _titleView = titleView;
        }

        public override BootState state => BootState.Load;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _titleView.push.FirstAsync(cancellationToken: token);
            _sceneUseCase.Load(SceneName.Top, LoadType.Fade);
            return BootState.None;
        }
    }
}