using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class StartUseCase : IDisposable
    {
        private readonly Subject<Unit> _pressStart;

        public StartUseCase()
        {
            _pressStart = new Subject<Unit>();
        }

        public void PressStart()
        {
            _pressStart?.OnNext(Unit.Default);
        }

        public async UniTask PressStartAsync(CancellationToken token)
        {
            await _pressStart.FirstAsync(token);
        }

        public void Dispose()
        {
            _pressStart?.Dispose();
        }
    }
}