using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class SetUpState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly BattleView _battleView;

        public SetUpState(CharacterUseCase characterUseCase, ParameterUseCase parameterUseCase, BattleView battleView)
        {
            _characterUseCase = characterUseCase;
            _parameterUseCase = parameterUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var (player, enemy) = _characterUseCase.GetBattleCharacters();
            await (
                _parameterUseCase.InitAsync(token),
                _battleView.CreatePlayerAsync(player, token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            return GameState.Deal;
        }
    }
}