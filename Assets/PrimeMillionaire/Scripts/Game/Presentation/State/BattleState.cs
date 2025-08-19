using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;

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

            await (
                _battleAnimationUseCase.DamageOrDeadAsync(attacker, token),
                _parameterUseCase.ApplyDamageAsync(token)
            );

            if (!_battleAnimationUseCase.IsDeath(attacker))
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
                        _battleAnimationUseCase.ExitAsync(Side.Enemy, token)
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