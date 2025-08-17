using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using PrimeMillionaire.Game.Presentation.View.Button;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class SortPresenter : IStartable, IDisposable
    {
        private readonly HandUseCase _handUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly SortButtonView _sortButtonView;
        private readonly TableView _tableView;
        private readonly CancellationTokenSource _tokenSource;

        public SortPresenter(HandUseCase handUseCase, OrderUseCase orderUseCase, SortButtonView sortButtonView,
            TableView tableView)
        {
            _handUseCase = handUseCase;
            _orderUseCase = orderUseCase;
            _sortButtonView = sortButtonView;
            _tableView = tableView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            _sortButtonView.push
                .Subscribe(_ => _handUseCase.SwitchSortAsync(_tokenSource.Token).Forget())
                .AddTo(_sortButtonView);

            Router.Default
                .SubscribeAwait<SortHandVO>(async (x, context) =>
                {
                    await _tableView.RepaintHandsAsync(x.side, x.hands, context.CancellationToken);

                    if (x.side == Side.Player)
                    {
                        for (int i = 0; i < HandConfig.ORDER_NUM; i++)
                        {
                            if (_orderUseCase.orders[i] == null) continue;
                            if (_orderUseCase.orders[i].card == null) continue;

                            var card = _orderUseCase.orders[i].card;
                            var index = x.hands.Select(y => y.card).ToList().IndexOf(card);
                            await _tableView.OrderHandsAsync(Side.Player, index, context.CancellationToken);
                            await _tableView.RenderOrderNo(Side.Player, index, i + 1, context.CancellationToken);
                        }
                    }
                })
                .AddTo(_tableView);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}