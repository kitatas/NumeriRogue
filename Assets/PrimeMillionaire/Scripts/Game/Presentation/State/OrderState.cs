using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
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
            await _orderUseCase.HideOrderCardsAsync(0.0f, token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _orderUseCase.ShowOrderCardsAsync(UiConfig.TWEEN_DURATION, token);

            while (true)
            {
                var (index, count) = await _tableView.OrderPlayerHandsAsync(token);
                var card = _handUseCase.GetPlayerCard(index);
                var orderNo = _orderUseCase.Set(card);
                await _tableView.RenderPlayerOrderNo(index, orderNo, token);

                if (count == HandConfig.ORDER_NUM) break;
            }

            {
                var index = await _tableView.TrashPlayerHandsAsync(token);
                _handUseCase.RemovePlayerCards(index);
            }

            _orderUseCase.StockBuff();
            await (
                _buffUseCase.ActivateBuffAsync(() =>
                {
                    _dollarUseCase.Update();
                    _parameterUseCase.PublishPlayerParamAsync(token).Forget();
                }, token),
                _orderUseCase.PushValueAsync(token)
            );

            var playerPt = _orderUseCase.currentValueWithBonus;
            await (
                _battlePtUseCase.AddPlayerBattlePtAsync(playerPt, token),
                _orderUseCase.RefreshAsync(token)
            );

            var enemyOrder = OrderHelper.GetOrder(_handUseCase.GetEnemyHands(), playerPt);
            foreach (var index in enemyOrder.index)
            {
                var card = _handUseCase.GetEnemyCard(index);
                _orderUseCase.Set(card);
                await _tableView.TrashEnemyHandAsync(index, token);
            }

            _handUseCase.RemoveEnemyCards(enemyOrder.index.OrderByDescending(x => x));

            await _orderUseCase.PushValueAsync(token);

            var enemyPt = _orderUseCase.currentValueWithBonus;
            await (
                _battlePtUseCase.AddEnemyBattlePtAsync(enemyPt, token),
                _orderUseCase.RefreshAsync(token)
            );

            _tableView.DestroyHideCards();

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