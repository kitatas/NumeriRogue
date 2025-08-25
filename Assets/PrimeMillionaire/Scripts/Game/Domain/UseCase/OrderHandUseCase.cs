using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderHandUseCase : IDisposable
    {
        private readonly Subject<int> _orderHand;
        private bool _isActivate;

        public OrderHandUseCase()
        {
            _isActivate = false;
            _orderHand = new Subject<int>();
        }

        public bool isActivate => _isActivate;

        public void Set(int index)
        {
            _orderHand?.OnNext(index);
        }

        public async UniTask<int> OrderHandAsync(CancellationToken token)
        {
            _isActivate = true;
            var index = await _orderHand.FirstAsync(cancellationToken: token);
            _isActivate = false;

            return index;
        }

        public void Dispose()
        {
            _orderHand?.Dispose();
        }
    }
}