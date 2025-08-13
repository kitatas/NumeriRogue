using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using PrimeMillionaire.Game.Utility;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BuffUseCase _buffUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly HandUseCase _handUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly TableView _tableView;

        public OrderState(BattlePtUseCase battlePtUseCase, BuffUseCase buffUseCase, DollarUseCase dollarUseCase,
            HandUseCase handUseCase, OrderUseCase orderUseCase, ParameterUseCase parameterUseCase, TableView tableView)
        {
            _battlePtUseCase = battlePtUseCase;
            _buffUseCase = buffUseCase;
            _dollarUseCase = dollarUseCase;
            _handUseCase = handUseCase;
            _orderUseCase = orderUseCase;
            _parameterUseCase = parameterUseCase;
            _tableView = tableView;
        }

        public override GameState state => GameState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await (
                _orderUseCase.HideOrderCardsAsync(0.0f, token),
                _tableView.DeactivatePlayerFieldAsync(0.0f, token)
            );
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await (
                _orderUseCase.ShowOrderCardsAsync(UiConfig.TWEEN_DURATION, token),
                _tableView.ActivatePlayerFieldAsync(UiConfig.TWEEN_DURATION, token)
            );

            while (true)
            {
                var (index, count) = await _tableView.OrderPlayerHandsAsync(token);
                var card = _handUseCase.GetCard(Side.Player, index);
                var orderNo = _orderUseCase.Set(card);
                await _tableView.RenderOrderNo(Side.Player, index, orderNo, token);

                if (count == HandConfig.ORDER_NUM) break;
            }

            {
                var index = await _tableView.TrashHandsAsync(Side.Player, token);
                _handUseCase.RemoveCards(Side.Player, index);
            }

            _orderUseCase.StockBuff();
            await (
                _buffUseCase.ActivateBuffAsync(() =>
                {
                    _dollarUseCase.Update();
                    _parameterUseCase.PublishPlayerParamAsync(token).Forget();
                }, token),
                _orderUseCase.PushValueAsync(token),
                _tableView.DeactivatePlayerFieldAsync(UiConfig.TWEEN_DURATION, token)
            );

            var playerPt = _orderUseCase.currentValueWithBonus;
            await (
                _battlePtUseCase.AddPlayerBattlePtAsync(playerPt, token),
                _orderUseCase.RefreshAsync(token)
            );

            var enemyOrder = OrderHelper.GetOrder(_handUseCase.GetHands(Side.Enemy), playerPt);
            foreach (var index in enemyOrder.index)
            {
                var card = _handUseCase.GetCard(Side.Enemy, index);
                var orderNo = _orderUseCase.Set(card);
                await _tableView.OrderEnemyHandsAsync(index, token);
                await _tableView.RenderOrderNo(Side.Enemy, index, orderNo, token);
            }

            {
                var index = await _tableView.TrashHandsAsync(Side.Enemy, token);
                _handUseCase.RemoveCards(Side.Enemy, index);
            }

            await _orderUseCase.PushValueAsync(token);

            var enemyPt = _orderUseCase.currentValueWithBonus;
            await (
                _battlePtUseCase.AddEnemyBattlePtAsync(enemyPt, token),
                _orderUseCase.RefreshAsync(token)
            );

            var isPlayerHandsEmpty = _handUseCase.IsPlayerHandsEmpty();
            if (isPlayerHandsEmpty)
            {
                await _orderUseCase.HideOrderCardsAsync(UiConfig.TWEEN_DURATION, token);
            }

            return isPlayerHandsEmpty
                ? GameState.Battle
                : GameState.Order;
        }
    }
}