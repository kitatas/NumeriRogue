using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
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
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly BattleView _battleView;

        public BattleState(BattlePtUseCase battlePtUseCase, BattleUseCase battleUseCase, DollarUseCase dollarUseCase,
            DropUseCase dropUseCase, EnemyCountUseCase enemyCountUseCase, BattleView battleView)
        {
            _battlePtUseCase = battlePtUseCase;
            _battleUseCase = battleUseCase;
            _dollarUseCase = dollarUseCase;
            _dropUseCase = dropUseCase;
            _enemyCountUseCase = enemyCountUseCase;
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

            if (!isDestroy)
            {
                await _battlePtUseCase.ResetAsync(token);
                return GameState.Deal;
            }

            switch (attacker)
            {
                case Side.Player:
                    _dollarUseCase.Add(_dropUseCase.GetDropDollar());
                    if (_enemyCountUseCase.IsClear()) return GameState.Clear;

                    await (
                        _battlePtUseCase.ResetAsync(token),
                        _battleView.DestroyEnemyAsync(token)
                    );
                    return GameState.Pick;
                case Side.Enemy:
                    return GameState.Fail;
                default:
                    throw new  QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE);
            }
        }
    }
}