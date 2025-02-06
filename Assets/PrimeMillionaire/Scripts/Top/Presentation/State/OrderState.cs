using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Top.Domain.UseCase;

namespace PrimeMillionaire.Top.Presentation.State
{
    public sealed class OrderState : BaseState
    {
        private readonly CharacterUseCase _characterUseCase;

        public OrderState(CharacterUseCase characterUseCase)
        {
            _characterUseCase = characterUseCase;
        }

        public override TopState state => TopState.Order;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<TopState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
            _characterUseCase.Order(CharacterType.Borealjuggernaut);

            return TopState.None;
        }
    }
}