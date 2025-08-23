using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderHandUseCase : IDisposable
    {
        private readonly Subject<int> _orderHand;

        public OrderHandUseCase()
        {
            _orderHand = new Subject<int>();
        }

        public void Set(int index)
        {
            _orderHand?.OnNext(index);
        }

        public async UniTask<int> OrderHandAsync(CancellationToken token)
        {
            return await _orderHand.FirstAsync(cancellationToken: token);
        }

        public void Dispose()
        {
            _orderHand?.Dispose();
        }
    }
}