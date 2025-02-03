using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.Presenter;
using PrimeMillionaire.Top.Presentation.State;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Installer
{
    public sealed class TopInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, InitState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<StatePresenter>();
            });
        }
    }
}