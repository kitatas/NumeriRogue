using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.Presenter;
using PrimeMillionaire.Top.Presentation.State;
using PrimeMillionaire.Top.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Installer
{
    public sealed class TopInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<CharacterUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, InitState>(Lifetime.Scoped);
            builder.Register<BaseState, OrderState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<CharacterPresenter>();
                entryPoints.Add<StatePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<CharacterListView>();
        }
    }
}