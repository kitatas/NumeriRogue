using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class SplashPresenter : IPostInitializable
    {
        private readonly SplashUseCase _splashUseCase;
        private readonly SplashView _splashView;

        public SplashPresenter(SplashUseCase splashUseCase, SplashView splashView)
        {
            _splashUseCase = splashUseCase;
            _splashView = splashView;
        }

        public void PostInitialize()
        {
            Router.Default
                .SubscribeAwait<SplashVO>(async (v, context) =>
                {
                    await _splashView.Render(v).WithCancellation(context.CancellationToken);
                })
                .AddTo(_splashView);

            _splashView.push
                .Subscribe(_ =>
                {
                    _splashView.Kill();
                    _splashUseCase.TouchScreen();
                })
                .AddTo(_splashView);
        }
    }
}