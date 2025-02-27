using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class RestartState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;

        private readonly CharacterUseCase _characterUseCase;
        private readonly DealUseCase _dealUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly TurnUseCase _turnUseCase;
        private readonly BattleView _battleView;

        public RestartState(InterruptUseCase interruptUseCase, CharacterUseCase characterUseCase,
            DealUseCase dealUseCase, EnemyCountUseCase enemyCountUseCase, ParameterUseCase parameterUseCase,
            TurnUseCase turnUseCase, BattleView battleView)
        {
            _interruptUseCase = interruptUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _parameterUseCase = parameterUseCase;
            _turnUseCase = turnUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.Restart;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _interruptUseCase.Load();

            // Init
            var player = _characterUseCase.GetPlayerCharacter();
            await (
                _parameterUseCase.PublishPlayerParamAsync(token),
                _battleView.CreatePlayerAsync(player, token)
            );

            // SetUp
            _enemyCountUseCase.Update();
            _turnUseCase.Update();

            var enemy = _characterUseCase.GetEnemyCharacter();
            await (
                _parameterUseCase.PublishEnemyParamAsync(token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            // Deal
            _dealUseCase.SetUpHands();
            // TODO: render hands

            return GameState.Order;
        }
    }
}