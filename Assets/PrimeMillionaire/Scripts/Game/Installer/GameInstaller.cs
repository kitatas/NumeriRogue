using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.Presenter;
using PrimeMillionaire.Game.Presentation.State;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Installer
{
    public sealed class GameInstaller : LifetimeScope
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