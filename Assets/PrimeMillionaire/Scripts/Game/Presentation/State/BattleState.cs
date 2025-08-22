using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Utility;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class BattleState : BaseState
    {
        private readonly BattleAnimationUseCase _battleAnimationUseCase;
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BattleUseCase _battleUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly DropUseCase _dropUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly ParameterUseCase _parameterUseCase;

        public BattleState(BattleAnimationUseCase battleAnimationUseCase, BattlePtUseCase battlePtUseCase,
            BattleUseCase battleUseCase, DollarUseCase dollarUseCase, DropUseCase dropUseCase,
            EnemyCountUseCase enemyCountUseCase, ParameterUseCase parameterUseCase)
        {
            _battleAnimationUseCase = battleAnimationUseCase;
            _battlePtUseCase = battlePtUseCase;
            _battleUseCase = battleUseCase;
            _dollarUseCase = dollarUseCase;
            _dropUseCase = dropUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _parameterUseCase = parameterUseCase;
        }

        public override GameState state => GameState.Battle;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var attacker = _battlePtUseCase.GetAttacker();
            _battleUseCase.ApplyDamage(attacker);
            await _battleAnimationUseCase.AttackAsync(attacker, token);

            var defender = attacker.ToOppositeSide();
            await (
                _battleAnimationUseCase.DamageOrDeadAsync(defender, token),
                _parameterUseCase.ApplyDamageAsync(token)
            );

            if (_battleAnimationUseCase.IsDeath(defender))
            {
                return defender switch
                {
                    Side.Player => GameState.Fail,
                    Side.Enemy => await HandleDefeatAsync(Side.Enemy, token),
                    _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE)
                };
            }

            await _battlePtUseCase.ResetAsync(token);
            return GameState.Deal;
        }

        private async UniTask<GameState> HandleDefeatAsync(Side side, CancellationToken token)
        {
            _dollarUseCase.Add(_dropUseCase.GetDropDollar());

            if (_enemyCountUseCase.IsClear())
            {
                return GameState.Clear;
            }

            await (
                _battlePtUseCase.ResetAsync(token),
                _battleAnimationUseCase.ExitAsync(side, token)
            );
            return GameState.Pick;
        }
    }
}