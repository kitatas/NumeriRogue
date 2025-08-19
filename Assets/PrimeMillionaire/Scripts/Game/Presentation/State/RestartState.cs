using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class RestartState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly LoadingUseCase _loadingUseCase;

        private readonly CharacterUseCase _characterUseCase;
        private readonly DealUseCase _dealUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly LevelUseCase _levelUseCase;
        private readonly HandUseCase _handUseCase;
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly TurnUseCase _turnUseCase;
        private readonly BattleView _battleView;
        private readonly TableView _tableView;

        public RestartState(InterruptUseCase interruptUseCase, LoadingUseCase loadingUseCase,
            CharacterUseCase characterUseCase, DealUseCase dealUseCase, DollarUseCase dollarUseCase,
            EnemyCountUseCase enemyCountUseCase, LevelUseCase levelUseCase, HandUseCase handUseCase,
            HoldSkillUseCase holdSkillUseCase, OrderUseCase orderUseCase, ParameterUseCase parameterUseCase,
            TurnUseCase turnUseCase, BattleView battleView, TableView tableView)
        {
            _interruptUseCase = interruptUseCase;
            _loadingUseCase = loadingUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _dollarUseCase = dollarUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _levelUseCase = levelUseCase;
            _handUseCase = handUseCase;
            _holdSkillUseCase = holdSkillUseCase;
            _orderUseCase = orderUseCase;
            _parameterUseCase = parameterUseCase;
            _turnUseCase = turnUseCase;
            _battleView = battleView;
            _tableView = tableView;
        }

        public override GameState state => GameState.Restart;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _interruptUseCase.Load();

            var player = _characterUseCase.GetCharacter(Side.Player);
            var stage = _characterUseCase.GetStage();
            var enemy = _characterUseCase.GetCharacter(Side.Enemy);

            _enemyCountUseCase.Update();
            _levelUseCase.Update();
            _turnUseCase.Update();
            _dollarUseCase.Update();
            _dealUseCase.SetUpHands();

            await (
                _parameterUseCase.PublishPlayerParamAsync(token),
                _battleView.CreateCharacterAsync(Side.Player, player, token),
                _battleView.RenderStageAsync(stage, token),
                _orderUseCase.PublishCommunityBattlePtAsync(token),
                _holdSkillUseCase.UpdateAsync(token),
                _parameterUseCase.PublishEnemyParamAsync(token),
                _battleView.CreateCharacterAsync(Side.Enemy, enemy, token),
                _tableView.SetUpAsync(0.0f, token)
            );

            await UniTask.WhenAll(
                HandConfig.ALL_SIDE.Select(x => _tableView.RenderHandsAsync(x, _handUseCase.GetHands(x), token))
            );

            _loadingUseCase.Set(false);

            return GameState.Order;
        }
    }
}