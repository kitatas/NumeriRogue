using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly BattleAnimationUseCase _battleAnimationUseCase;
        private readonly CharacterUseCase _characterUseCase;
        private readonly DealUseCase _dealUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly ISoundUseCase _soundUseCase;
        private readonly StageUseCase _stageUseCase;

        public InitState(BattleAnimationUseCase battleAnimationUseCase, CharacterUseCase characterUseCase,
            DealUseCase dealUseCase, LoadingUseCase loadingUseCase, ParameterUseCase parameterUseCase,
            ISoundUseCase soundUseCase, StageUseCase stageUseCase)
        {
            _battleAnimationUseCase = battleAnimationUseCase;
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _loadingUseCase = loadingUseCase;
            _parameterUseCase = parameterUseCase;
            _soundUseCase = soundUseCase;
            _stageUseCase = stageUseCase;
        }

        public override GameState state => GameState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _dealUseCase.Init();

            await (
                _parameterUseCase.InitPlayerParamAsync(token),
                _stageUseCase.PublishStageAsync(token)
            );

            _loadingUseCase.Set(false);

            _soundUseCase.Play(Bgm.Game);

            await UniTaskHelper.DelayAsync(1.0f, token);

            var player = _characterUseCase.GetCharacter(Side.Player);
            await _battleAnimationUseCase.EntryAsync(Side.Player, player, token);
            await UniTaskHelper.DelayAsync(0.5f, token);

            return GameState.SetUp;
        }
    }
}