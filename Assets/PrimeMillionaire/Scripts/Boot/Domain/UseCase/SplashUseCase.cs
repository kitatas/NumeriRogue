using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.Repository;
using R3;
using VitalRouter;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class SplashUseCase : IDisposable
    {
        private readonly SplashRepository _splashRepository;
        private readonly Subject<Unit> _touchScreen;
        private readonly ReactiveProperty<bool> _activation;

        public SplashUseCase(SplashRepository splashRepository)
        {
            _splashRepository = splashRepository;
            _touchScreen = new Subject<Unit>();
            _activation = new ReactiveProperty<bool>(true);
        }

        public Observable<bool> activation => _activation;

        public void TouchScreen()
        {
            _touchScreen?.OnNext(Unit.Default);
        }

        public async UniTask SequentialRenderAsync(CancellationToken token)
        {
            foreach (var type in SplashConfig.TYPES)
            {
                var sprite = _splashRepository.Find(type);
                await UniTask.WhenAny(
                    Router.Default.PublishAsync(new SplashVO(sprite, SplashConfig.DURATION, true), token).AsUniTask(),
                    _touchScreen.FirstAsync(cancellationToken: token).AsUniTask()
                );
            }
        }

        public void Activate(bool value)
        {
            _activation?.OnNext(value);
        }

        public void Dispose()
        {
            _touchScreen?.Dispose();
            _activation?.Dispose();
        }
    }
}