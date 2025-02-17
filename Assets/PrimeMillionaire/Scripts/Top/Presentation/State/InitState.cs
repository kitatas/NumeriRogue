using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Top.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class InitState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;

        public InitState(CharacterUseCase characterUseCase)
        {
            _characterUseCase = characterUseCase;
        }

        public override TopState state => TopState.Init;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _characterUseCase.Order(1.ToCharacterType());
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            return TopState.Order;
        }
    }
}