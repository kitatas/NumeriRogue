using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePresenter : IStartable
    {
        private readonly BattleView _battleView;

        public BattlePresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<BattleAnimationVO>(async (x, context) =>
                {
                    switch (x.battleAnim)
                    {
                        case BattleAnim.Entry:
                            await _battleView.CreateCharacterAsync(x.side, x.character, context.CancellationToken);
                            break;
                        case BattleAnim.Exit:
                            await _battleView.DestroyCharacterAsync(x.side, context.CancellationToken);
                            break;
                        case BattleAnim.Attack:
                            await _battleView.PlayAttackAnimAsync(x.side, context.CancellationToken);
                            break;
                        case BattleAnim.Hit:
                            await _battleView.PlayDamageAnimAsync(x.side, false, context.CancellationToken);
                            break;
                        case BattleAnim.Death:
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