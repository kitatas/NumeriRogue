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
        private readonly ParameterUseCase _parameterUseCase;
        private readonly BattleView _battleView;

        public RestartState(InterruptUseCase interruptUseCase, CharacterUseCase characterUseCase,
            DealUseCase dealUseCase, ParameterUseCase parameterUseCase, BattleView battleView)
        {
            _interruptUseCase = interruptUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _parameterUseCase = parameterUseCase;
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
            _dealUseCase.Init();

            var player = _characterUseCase.GetPlayerCharacter();
            await (
                _parameterUseCase.PublishPlayerParamAsync(token),
                _battleView.CreatePlayerAsync(player, token)
            );

            // SetUp
            // TODO: enemy count
            // TODO: turn count
            var enemy = _characterUseCase.GetEnemyCharacter();
            await (
                _parameterUseCase.PublishEnemyParamAsync(token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            // Deal
            // TODO: deal cards
            // TODO: render hands

            return GameState.Order;
        }
    }
}