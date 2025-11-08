using PrimeMillionaire.Boot.Data.DataStore;
using PrimeMillionaire.Boot.Domain.Repository;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.Presenter;
using PrimeMillionaire.Boot.Presentation.State;
using PrimeMillionaire.Boot.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        [SerializeField] private SplashTable splashTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<SplashTable>(splashTable);

            // Repository
            builder.Register<AppVersionRepository>(Lifetime.Scoped);
            builder.Register<SplashRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<MasterUseCase>(Lifetime.Scoped);
            builder.Register<InterruptUseCase>(Lifetime.Scoped);
            builder.Register<LoginUseCase>(Lifetime.Scoped);
            builder.Register<ModalUseCase>(Lifetime.Scoped);
            builder.Register<SplashUseCase>(Lifetime.Scoped);
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
                entryPoints.Add<SplashPresenter>();
                entryPoints.Add<StatePresenter>();
                entryPoints.Add<TitlePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<TitleView>();
            builder.RegisterComponentInHierarchy<InterruptView>();
            builder.RegisterComponentInHierarchy<SplashView>();
        }
    }
}