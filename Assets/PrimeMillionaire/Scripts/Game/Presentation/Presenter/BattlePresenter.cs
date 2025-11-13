using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePresenter : IPostInitializable
    {
        private readonly ISoundUseCase _soundUseCase;
        private readonly BattleView _battleView;

        public BattlePresenter(ISoundUseCase soundUseCase, BattleView battleView)
        {
            _soundUseCase = soundUseCase;
            _battleView = battleView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<BattleAnimationVO>(async (x, context) =>
                {
                    switch (x.battleAnim)
                    {
                        case BattleAnim.Entry:
                            _soundUseCase.Play(Se.Entry);
                            await _battleView.CreateCharacterAsync(x.side, x.character, context.CancellationToken);
                            break;
                        case BattleAnim.Exit:
                            _soundUseCase.Play(Se.Explode);
                            await _battleView.DestroyCharacterAsync(x.side, context.CancellationToken);
                            break;
                        case BattleAnim.Attack:
                            await _battleView.PlayAttackAnimAsync(x.side, context.CancellationToken);
                            break;
                        case BattleAnim.Hit:
                            _soundUseCase.Play(Se.Hit);
                            await _battleView.PlayDamageAnimAsync(x.side, false, context.CancellationToken);
                            break;
                        case BattleAnim.Death:
                            _soundUseCase.Play(Se.Hit);
                            await _battleView.PlayDamageAnimAsync(x.side, true, context.CancellationToken);
                            break;
                        default:
                            throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BATTLE_ANIMATION);
                    }
                })
                .AddTo(_battleView);
        }
    }
}