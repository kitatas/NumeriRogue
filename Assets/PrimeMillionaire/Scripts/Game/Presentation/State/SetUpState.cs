using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class SetUpState : BaseState
    {
        private readonly BattleAnimationUseCase _battleAnimationUseCase;
        private readonly CharacterUseCase _characterUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly LevelUseCase _levelUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly TurnUseCase _turnUseCase;

        public SetUpState(BattleAnimationUseCase battleAnimationUseCase, CharacterUseCase characterUseCase,
            EnemyCountUseCase enemyCountUseCase, LevelUseCase levelUseCase, ParameterUseCase parameterUseCase,
            TurnUseCase turnUseCase)
        {
            _battleAnimationUseCase = battleAnimationUseCase;
            _characterUseCase = characterUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _levelUseCase = levelUseCase;
            _parameterUseCase = parameterUseCase;
            _turnUseCase = turnUseCase;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _enemyCountUseCase.Increment();
            _levelUseCase.Lot();
            _turnUseCase.Reset();

            var enemy = _characterUseCase.LotEnemyCharacter();
            await (
                _parameterUseCase.InitEnemyParamAsync(token),
                _battleAnimationUseCase.EntryAsync(Side.Enemy, enemy, token)
            );

            return GameState.Deal;
        }
    }
}