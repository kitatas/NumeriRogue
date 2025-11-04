using PrimeMillionaire.Boot.Domain.Repository;
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
            // Repository
            builder.Register<AppVersionRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<MasterUseCase>(Lifetime.Scoped);
            builder.Register<InterruptUseCase>(Lifetime.Scoped);
            builder.Register<LoginUseCase>(Lifetime.Scoped);
            builder.Register<ModalUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<TitleUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, CheckState>(Lifetime.Scoped);
            builder.Register<BaseState, InterruptState>(Lifetime.Scoped);
            builder.Register<BaseState, LoadState>(Lifetime.Scoped);
            builder.Register<BaseState, LoginState>(Lifetime.Scoped);
            builder.Register<BaseState, StartState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<InterruptPresenter>();
                entryPoints.Add<ModalPresenter>();
                entryPoints.Add<SoundPresenter>();
                entryPoints.Add<StatePresenter>();
                entryPoints.Add<TitlePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<TitleView>();
            builder.RegisterComponentInHierarchy<InterruptView>();
        }
    }
}