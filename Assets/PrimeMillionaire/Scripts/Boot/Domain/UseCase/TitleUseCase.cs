using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class TitleUseCase : IDisposable
    {
        private readonly Subject<Unit> _touchScreen;

        public TitleUseCase()
        {
            _touchScreen = new Subject<Unit>();
        }

        public void TouchScreen()
        {
            _touchScreen?.OnNext(Unit.Default);
        }

        public async UniTask TouchScreenAsync(CancellationToken token)
        {
            await _touchScreen.FirstAsync(cancellationToken: token);
        }

        public void Dispose()
        {
            _touchScreen?.Dispose();
        }
    }
}