using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class SetUpState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly BattleView _battleView;

        public SetUpState(CharacterUseCase characterUseCase, BattleView battleView)
        {
            _characterUseCase = characterUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await _characterUseCase.InitAsync(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var (player, enemy) = _characterUseCase.GetBattleCharacters();
            await (
                _battleView.CreatePlayerAsync(player, token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            return GameState.Deal;
        }
    }
}