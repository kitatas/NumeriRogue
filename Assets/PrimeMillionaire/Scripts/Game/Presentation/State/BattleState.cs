using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class BattleState : BaseState
    {
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BattleView _battleView;

        public BattleState(BattlePtUseCase battlePtUseCase, BattleView battleView)
        {
            _battlePtUseCase = battlePtUseCase;
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
            var isDestroy = false;
            _battleView.PlayAnimation(attacker, isDestroy);

            await UniTaskHelper.DelayAsync(2.0f, token);

            await _battlePtUseCase.ResetAsync(token);

            return isDestroy
                ? GameState.None
                : GameState.Deal;
        }
    }
}