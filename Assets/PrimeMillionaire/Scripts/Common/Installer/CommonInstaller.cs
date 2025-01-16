using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.Presenter;
using PrimeMillionaire.Common.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Installer
{
    public sealed class CommonInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<ScenePresenter>();
            });

            // View
            builder.RegisterInstance<TransitionView>(FindFirstObjectByType<TransitionView>());
        }
    }
}