using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class DealState : BaseState
    {
        private readonly HandUseCase _handUseCase;

        public DealState(HandUseCase handUseCase)
        {
            _handUseCase = handUseCase;
        }

        public override GameState state => GameState.Deal;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _handUseCase.SetUp();

            // TODO: 手札の描画
            var playerHands = _handUseCase.GetPlayerHands();
            var enemyHands = _handUseCase.GetEnemyHands();

            await UniTask.Yield(token);

            return GameState.None;
        }
    }
}