using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Top.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly StartUseCase _startUseCase;

        public OrderState(SceneUseCase sceneUseCase, StartUseCase startUseCase)
        {
            _sceneUseCase = sceneUseCase;
            _startUseCase = startUseCase;
        }

        public override TopState state => TopState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await _startUseCase.PressStartAsync(token);
            _sceneUseCase.Load(SceneName.Game, LoadType.Fade);
            return TopState.None;
        }
    }
}