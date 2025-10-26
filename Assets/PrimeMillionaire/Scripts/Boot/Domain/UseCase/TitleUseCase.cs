using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class TitleUseCase : IDisposable
    {
        private readonly Subject<Unit> _touchScreen;
        private readonly Subject<bool> _playTouchScreen;

        public TitleUseCase()
        {
            _touchScreen = new Subject<Unit>();
            _playTouchScreen = new Subject<bool>();
        }

        public Observable<bool> playTouchScreen => _playTouchScreen;

        public void TouchScreen()
        {
            _touchScreen?.OnNext(Unit.Default);
        }

        public async UniTask TouchScreenAsync(CancellationToken token)
        {
            _playTouchScreen?.OnNext(true);
            await _touchScreen.FirstAsync(cancellationToken: token);
        }

        public void Dispose()
        {
            _touchScreen?.Dispose();
            _playTouchScreen?.Dispose();
        }
    }
}