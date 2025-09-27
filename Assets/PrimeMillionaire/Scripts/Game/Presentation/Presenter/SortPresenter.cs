using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class SortPresenter : IPostInitializable, IDisposable
    {
        private readonly HandUseCase _handUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly TableView _tableView;
        private readonly CancellationTokenSource _tokenSource;

        public SortPresenter(HandUseCase handUseCase, OrderUseCase orderUseCase, TableView tableView)
        {
            _handUseCase = handUseCase;
            _orderUseCase = orderUseCase;
            _tableView = tableView;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _handUseCase.sort
                .Subscribe(_tableView.SwitchBackground)
                .AddTo(_tableView);

            _tableView.pushAnySort
                .Subscribe(x => _handUseCase.SetSortAsync(x, _tokenSource.Token).Forget())
                .AddTo(_tableView);

            Router.Default
                .SubscribeAwait<SortHandVO>(async (x, context) =>
                {
                    await _tableView.RepaintHandsAsync(x.side, x.hands, context.CancellationToken);

                    if (x.side == Side.Player)
                    {
                        await RepaintHandsOrderAsync(x, context.CancellationToken);
                    }
                })
                .AddTo(_tableView);
        }

        private async UniTask RepaintHandsOrderAsync(SortHandVO sortHand, CancellationToken token)
        {
            for (int i = 0; i < HandConfig.ORDER_NUM; i++)
            {
                var card = _orderUseCase.orders[i]?.card;
                if (card == null) continue;

                var index = sortHand.hands.Select(y => y.card).ToList().IndexOf(card);
                _orderUseCase.SetOrder(sortHand.side, i, index, card);
                await _tableView.OrderHandsAsync(Side.Player, index, token);
                await _tableView.RenderOrderNo(Side.Player, index, i + 1, token);
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}