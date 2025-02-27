using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.Presenter;
using PrimeMillionaire.Boot.Presentation.State;
using PrimeMillionaire.Boot.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, LoadState>(Lifetime.Scoped);
            builder.Register<BaseState, LoginState>(Lifetime.Scoped);
            builder.Register<BaseState, RestartState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<StatePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<TitleView>();
        }
    }
}