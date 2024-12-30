using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly HandUseCase _handUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly TableView _tableView;

        public OrderState(HandUseCase handUseCase, OrderUseCase orderUseCase, TableView tableView)
        {
            _handUseCase = handUseCase;
            _orderUseCase = orderUseCase;
            _tableView = tableView;
        }

        public override GameState state => GameState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (true)
            {
                var (index, count) = await _tableView.OrderPlayerHandsAsync(token);
                var card = _handUseCase.GetPlayerCard(index);
                _orderUseCase.Set(card);

                if (count == HandConfig.ORDER_NUM) break;
            }

            return GameState.None;
        }
    }
}