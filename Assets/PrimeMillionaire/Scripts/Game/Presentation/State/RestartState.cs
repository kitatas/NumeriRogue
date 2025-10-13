using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class RestartState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly LoadingUseCase _loadingUseCase;

        private readonly BattleAnimationUseCase _battleAnimationUseCase;
        private readonly CharacterUseCase _characterUseCase;
        private readonly DealUseCase _dealUseCase;
        private readonly DollarUseCase _dollarUseCase;
        private readonly EnemyCountUseCase _enemyCountUseCase;
        private readonly LevelUseCase _levelUseCase;
        private readonly HandUseCase _handUseCase;
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly OrderUseCase _orderUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly ISoundUseCase _soundUseCase;
        private readonly StageUseCase _stageUseCase;
        private readonly TurnUseCase _turnUseCase;

        public RestartState(InterruptUseCase interruptUseCase, LoadingUseCase loadingUseCase,
            BattleAnimationUseCase battleAnimationUseCase, CharacterUseCase characterUseCase, DealUseCase dealUseCase,
            DollarUseCase dollarUseCase, EnemyCountUseCase enemyCountUseCase, LevelUseCase levelUseCase,
            HandUseCase handUseCase, HoldSkillUseCase holdSkillUseCase, OrderUseCase orderUseCase,
            ParameterUseCase parameterUseCase, ISoundUseCase soundUseCase, StageUseCase stageUseCase,
            TurnUseCase turnUseCase)
        {
            _interruptUseCase = interruptUseCase;
            _loadingUseCase = loadingUseCase;
            _battleAnimationUseCase = battleAnimationUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _dollarUseCase = dollarUseCase;
            _enemyCountUseCase = enemyCountUseCase;
            _levelUseCase = levelUseCase;
            _handUseCase = handUseCase;
            _holdSkillUseCase = holdSkillUseCase;
            _orderUseCase = orderUseCase;
            _parameterUseCase = parameterUseCase;
            _soundUseCase = soundUseCase;
            _stageUseCase = stageUseCase;
            _turnUseCase = turnUseCase;
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
            var enemy = _characterUseCase.GetCharacter(Side.Enemy);

            _enemyCountUseCase.Update();
            _levelUseCase.Update();
            _turnUseCase.Update();
            _dollarUseCase.Update();
            _dealUseCase.SetUpHands();

            await (
                _battleAnimationUseCase.EntryAsync(Side.Player, player, token),
                _battleAnimationUseCase.EntryAsync(Side.Enemy, enemy, token),
                _holdSkillUseCase.UpdateAsync(token),
                _orderUseCase.PublishCommunityBattlePtAsync(token),
                _parameterUseCase.PublishPlayerParamAsync(token),
                _parameterUseCase.PublishEnemyParamAsync(token),
                _stageUseCase.PublishStageAsync(token)
            );

            await _handUseCase.DealAsync(token);

            _loadingUseCase.Set(false);

            _soundUseCase.Play(Bgm.Game);

            return GameState.Order;
        }
    }
}