using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePresenter : IPostInitializable
    {
        private readonly BattleView _battleView;

        public BattlePresenter(BattleView battleView)
        {
            _battleView = battleView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<BattleAnimationVO>(async (x, context) =>
                {
                    await (x.battleAnim switch
                    {
                        BattleAnim.Entry => _battleView.CreateCharacterAsync(x.side, x.character, context.CancellationToken),
                        BattleAnim.Exit => _battleView.DestroyCharacterAsync(x.side, context.CancellationToken),
                        BattleAnim.Attack => _battleView.PlayAttackAnimAsync(x.side, context.CancellationToken),
                        BattleAnim.Hit => _battleView.PlayDamageAnimAsync(x.side, false, context.CancellationToken),
                        BattleAnim.Death => _battleView.PlayDamageAnimAsync(x.side, true, context.CancellationToken),
                        _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BATTLE_ANIMATION),
                    });
                })
                .AddTo(_battleView);
        }
    }
}