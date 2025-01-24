using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class SetUpState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly TurnUseCase _turnUseCase;
        private readonly BattleView _battleView;

        public SetUpState(CharacterUseCase characterUseCase, EnemyCountUseCase enemyCountUseCase,
            ParameterUseCase parameterUseCase, TurnUseCase turnUseCase, BattleView battleView)
        {
            _characterUseCase = characterUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _parameterUseCase = parameterUseCase;
            _turnUseCase = turnUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _enemyCountUseCase.Increment();
            _turnUseCase.Reset();

            var enemy = _characterUseCase.GetEnemyCharacter();
            await (
                _parameterUseCase.InitEnemyParamAsync(token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            return GameState.Deal;
        }
    }
}