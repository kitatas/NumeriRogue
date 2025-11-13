using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class OrderPresenter : IPostInitializable
    {
        private readonly ISoundUseCase _soundUseCase;
        private readonly OrderView _orderView;
        private readonly TableView _tableView;

        public OrderPresenter(ISoundUseCase soundUseCase, OrderView orderView, TableView tableView)
        {
            _soundUseCase = soundUseCase;
            _orderView = orderView;
            _tableView = tableView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<OrderVO>(async (x, context) =>
                {
                    _soundUseCase.Play(Se.Flip);
                    await _orderView.RenderAsync(x.orderIndex, x.card, context.CancellationToken);

                    if (x.side != Side.None)
                    {
                        await _tableView.RenderOrderNo(x.side, x.handIndex, x.no, context.CancellationToken);
                    }
                })
                .AddTo(_orderView);

            Router.Default
                .SubscribeAwait<OrderValueVO>(async (x, context) =>
                {
                    await _orderView.SetAsync(x, se => _soundUseCase.Play(se), context.CancellationToken);
                })
                .AddTo(_orderView);

            Router.Default
                .SubscribeAwait<OrderCardsFadeVO>(async (x, context) =>
                {
                    await (x.type switch
                    {
                        DisplayType.Show => _orderView.FadeOutCardsAsync(x.duration, context.CancellationToken),
                        DisplayType.Hide => _orderView.FadeInCardsAsync(x.duration, context.CancellationToken),
                        _ => throw new RebootExceptionVO(ExceptionConfig.NOT_FOUND_FADE),
                    });
                })
                .AddTo(_orderView);

            _orderView.Init();
        }
    }
}