using PrimeMillionaire.Top.Domain.Repository;
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
            // Repository
            builder.Register<LicenseRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<CharacterUseCase>(Lifetime.Scoped);
            builder.Register<LicenseUseCase>(Lifetime.Scoped);
            builder.Register<ModalUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, InitState>(Lifetime.Scoped);
            builder.Register<BaseState, OrderState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<CharacterPresenter>();
                entryPoints.Add<LicensePresenter>();
                entryPoints.Add<ModalPresenter>();
                entryPoints.Add<StatePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<CharacterScrollView>();
            builder.RegisterComponentInHierarchy<LicenseView>();
            builder.RegisterComponentInHierarchy<OrderCharacterView>();
            builder.RegisterComponentInHierarchy<OrderView>();
        }
    }
}