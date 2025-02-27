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
        private readonly ParameterUseCase _parameterUseCase;
        private readonly BattleView _battleView;

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
            // TODO: player parameter
            await (
                _parameterUseCase.InitPlayerParamAsync(token),
                _battleView.CreatePlayerAsync(player, token)
            );

            // SetUp
            // TODO: enemy count
            // TODO: turn count
            var enemy = _characterUseCase.GetEnemyCharacter();
            // TODO: enemy parameter
            await (
                _parameterUseCase.InitEnemyParamAsync(token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            // Deal
            // TODO: deal cards
            // TODO: render hands

            return GameState.Order;
        }
    }
}