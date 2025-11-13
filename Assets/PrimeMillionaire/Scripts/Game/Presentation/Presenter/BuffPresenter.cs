using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BuffPresenter : IPostInitializable
    {
        private readonly ISoundUseCase _soundUseCase;
        private readonly BattleView _battleView;

        public BuffPresenter(ISoundUseCase soundUseCase, BattleView battleView)
        {
            _soundUseCase = soundUseCase;
            _battleView = battleView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<BuffVO>(async (x, context) =>
                {
                    _soundUseCase.Play(Se.Buff);
                    _battleView.PlayBuff(x);
                    await UniTaskHelper.DelayFrameAsync(20, context.CancellationToken);
                })
                .AddTo(_battleView);
        }
    }
}