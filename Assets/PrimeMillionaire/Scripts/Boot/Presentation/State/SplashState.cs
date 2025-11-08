using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class SplashState : BaseState
    {
        private readonly SplashUseCase _splashUseCase;

        public SplashState(SplashUseCase splashUseCase)
        {
            _splashUseCase = splashUseCase;
        }

        public override BootState state => BootState.Splash;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _splashUseCase.SequentialRenderAsync(token);

            return BootState.Login;
        }
    }
}