using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Utility;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BuffUseCase _buffUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly HandUseCase _handUseCase;
        private readonly OrderHandUseCase _orderHandUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly ParameterUseCase _parameterUseCase;

        public OrderState(BattlePtUseCase battlePtUseCase, BuffUseCase buffUseCase, DollarUseCase dollarUseCase,
            HandUseCase handUseCase, OrderHandUseCase orderHandUseCase, OrderUseCase orderUseCase,
            ParameterUseCase parameterUseCase)
        {
            _battlePtUseCase = battlePtUseCase;
            _buffUseCase = buffUseCase;
            _dollarUseCase = dollarUseCase;
            _handUseCase = handUseCase;
            _orderHandUseCase = orderHandUseCase;
            _orderUseCase = orderUseCase;
            _parameterUseCase = parameterUseCase;
        }

        public override GameState state => GameState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await (
                _handUseCase.DisplayPlayerHandFieldAsync(DisplayType.Hide, 0.0f, token),
                _orderUseCase.DisplayOrderCardsAsync(DisplayType.Hide, 0.0f, token)
            );
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await (
                _handUseCase.DisplayPlayerHandFieldAsync(DisplayType.Show, UiConfig.TWEEN_DURATION, token),
                _orderUseCase.DisplayOrderCardsAsync(DisplayType.Show, UiConfig.TWEEN_DURATION, token)
            );

            while (true)
            {
                var index = await _orderHandUseCase.OrderHandAsync(token);
                var card = _handUseCase.GetCard(Side.Player, index);
                await _orderUseCase.SetAsync(Side.Player, index, card, token);

                if (_orderUseCase.isAllOrder) break;
            }

            await _handUseCase.RemoveCardsAsync(Side.Player, _orderUseCase.orderHandIndex, token);

            _orderUseCase.StockBuff();
            await (
                _buffUseCase.ActivateBuffAsync(() =>
                {
                    _dollarUseCase.Update();
                    _parameterUseCase.PublishPlayerParamAsync(token).Forget();
                }, token),
                _handUseCase.DisplayPlayerHandFieldAsync(DisplayType.Hide, UiConfig.TWEEN_DURATION, token),
                _orderUseCase.PushValueAsync(token)
            );

            var playerPt = await ApplyBattlePtAsync(Side.Player, token);

            var enemyOrder = OrderHelper.GetOrder(_handUseCase.GetHands(Side.Enemy), playerPt);
            foreach (var index in enemyOrder.index)
            {
                var card = _handUseCase.GetCard(Side.Enemy, index);
                await _orderUseCase.SetAsync(Side.Enemy, index, card, token);
                await UniTaskHelper.DelayAsync(CardConfig.FLIP_DURATION, token);
            }

            await _handUseCase.RemoveCardsAsync(Side.Enemy, _orderUseCase.orderHandIndex, token);

            await _orderUseCase.PushValueAsync(token);

            await ApplyBattlePtAsync(Side.Enemy, token);

            if (_handUseCase.IsPlayerHandsEmpty())
            {
                await _orderUseCase.DisplayOrderCardsAsync(DisplayType.Hide, UiConfig.TWEEN_DURATION, token);
                return GameState.Battle;
            }
            else
            {
                return GameState.Order;
            }
        }

        private async UniTask<int> ApplyBattlePtAsync(Side side, CancellationToken token)
        {
            var pt = _orderUseCase.currentValueWithBonus;
            await (
                _battlePtUseCase.AddBattlePtAsync(side, pt, token),
                _orderUseCase.RefreshAsync(token)
            );

            return pt;
        }
    }
}