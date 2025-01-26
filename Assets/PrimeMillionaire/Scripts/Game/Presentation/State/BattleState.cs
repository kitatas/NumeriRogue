using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class BattleState : BaseState
    {
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BattleUseCase _battleUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly DropUseCase _dropUseCase;
        private readonly BattleView _battleView;

        public BattleState(BattlePtUseCase battlePtUseCase, BattleUseCase battleUseCase, DollarUseCase dollarUseCase,
            DropUseCase dropUseCase, BattleView battleView)
        {
            _battlePtUseCase = battlePtUseCase;
            _battleUseCase = battleUseCase;
            _dollarUseCase = dollarUseCase;
            _dropUseCase = dropUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.Battle;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var attacker = _battlePtUseCase.GetAttacker();
            var isDestroy = _battleUseCase.IsDestroy();
            await _battleView.PlayAttackAnimAsync(attacker, token);

            await (
                _battleView.PlayDamageAnimAsync(attacker, isDestroy, token),
                _battleUseCase.ExecBattleAsync(token)
            );

            await _battlePtUseCase.ResetAsync(token);

            if (isDestroy)
            {
                _dollarUseCase.Add(_dropUseCase.GetDropDollar());
                _battleView.DestroyEnemy();
            }

            return isDestroy
                ? GameState.Pick
                : GameState.Deal;
        }
    }
}