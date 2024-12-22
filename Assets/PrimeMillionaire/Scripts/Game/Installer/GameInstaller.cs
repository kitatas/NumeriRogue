using PrimeMillionaire.Game.Data.DataStore;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.Presenter;
using PrimeMillionaire.Game.Presentation.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Installer
{
    public sealed class GameInstaller : LifetimeScope
    {
        [SerializeField] private CardTable cardTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<CardTable>(cardTable);

            // Entity
            builder.Register<DeckEntity>(Lifetime.Scoped);

            // Repository
            builder.Register<CardRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<DealUseCase>(Lifetime.Scoped);
            builder.Register<HandUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, DealState>(Lifetime.Scoped);
            builder.Register<BaseState, InitState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<StatePresenter>();
            });
        }
    }
}