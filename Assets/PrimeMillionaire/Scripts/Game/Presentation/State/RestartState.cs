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
            LevelUseCase levelUseCase, HandUseCase handUseCase, HoldSkillUseCase holdSkillUseCase,
            OrderUseCase orderUseCase, ParameterUseCase parameterUseCase, TurnUseCase turnUseCase,
            BattleView battleView, TableView tableView)
        {
            _interruptUseCase = interruptUseCase;
            _loadingUseCase = loadingUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _dollarUseCase = dollarUseCase;
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

            // Init
            var player = _characterUseCase.GetPlayerCharacter();
            var stage = _characterUseCase.GetStage();
            await (
                _parameterUseCase.PublishPlayerParamAsync(token),
                _battleView.CreatePlayerAsync(player, token),
                _battleView.RenderStageAsync(stage, token),
                _orderUseCase.PublishCommunityBattlePtAsync(token)
            );

            await _holdSkillUseCase.UpdateAsync(token);

            // SetUp
            _levelUseCase.Update();
            _turnUseCase.Update();
            _dollarUseCase.Update();

            var enemy = _characterUseCase.GetEnemyCharacter();
            await (
                _parameterUseCase.PublishEnemyParamAsync(token),
                _battleView.CreateEnemyAsync(enemy, token)
            );

            // Deal
            _dealUseCase.SetUpHands();
            await _tableView.SetUpAsync(token);

            await (
                _tableView.RenderPlayerHandsAsync(_handUseCase.GetPlayerHands(), token),
                _tableView.RenderEnemyHandsAsync(_handUseCase.GetEnemyHands(), token)
            );

            _loadingUseCase.Set(false);

            return GameState.Order;
        }
    }
}