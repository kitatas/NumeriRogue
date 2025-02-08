using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly OrderView _orderView;

        public OrderState(SceneUseCase sceneUseCase, OrderView orderView)
        {
            _sceneUseCase = sceneUseCase;
            _orderView = orderView;
        }

        public override TopState state => TopState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await _orderView.push.FirstAsync(cancellationToken: token);
            _sceneUseCase.Load(SceneName.Game, LoadType.Fade);
            return TopState.None;
        }
    }
}