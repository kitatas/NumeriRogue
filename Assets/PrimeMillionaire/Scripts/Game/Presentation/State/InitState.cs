using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly DealUseCase _dealUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly ParameterUseCase _parameterUseCase;
        private readonly BattleView _battleView;

        public InitState(CharacterUseCase characterUseCase, DealUseCase dealUseCase, LoadingUseCase loadingUseCase,
            ParameterUseCase parameterUseCase, BattleView battleView)
        {
            _characterUseCase = characterUseCase;
            _dealUseCase = dealUseCase;
            _loadingUseCase = loadingUseCase;
            _parameterUseCase = parameterUseCase;
            _battleView = battleView;
        }

        public override GameState state => GameState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _dealUseCase.Init();

            var player = _characterUseCase.GetPlayerCharacter();
            var stage = _characterUseCase.GetStage();
            await (
                _parameterUseCase.InitPlayerParamAsync(token),
                _battleView.CreatePlayerAsync(player, token),
                _battleView.RenderStageAsync(stage, token)
            );

            _loadingUseCase.Set(false);

            return GameState.SetUp;
        }
    }
}