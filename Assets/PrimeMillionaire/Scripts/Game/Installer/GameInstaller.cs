using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.Presenter;
using PrimeMillionaire.Game.Presentation.State;
using PrimeMillionaire.Game.Presentation.View;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Installer
{
    public sealed class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Entity
            builder.Register<DeckEntity>(Lifetime.Scoped);
            builder.Register<EnemyBattlePtEntity>(Lifetime.Scoped);
            builder.Register<EnemyHandEntity>(Lifetime.Scoped);
            builder.Register<PlayerBattlePtEntity>(Lifetime.Scoped);
            builder.Register<PlayerHandEntity>(Lifetime.Scoped);

            // Repository
            builder.Register<CardRepository>(Lifetime.Scoped);
            builder.Register<CharacterRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BattlePtUseCase>(Lifetime.Scoped);
            builder.Register<CharacterUseCase>(Lifetime.Scoped);
            builder.Register<DealUseCase>(Lifetime.Scoped);
            builder.Register<HandUseCase>(Lifetime.Scoped);
            builder.Register<OrderUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, BattleState>(Lifetime.Scoped);
            builder.Register<BaseState, DealState>(Lifetime.Scoped);
            builder.Register<BaseState, InitState>(Lifetime.Scoped);
            builder.Register<BaseState, OrderState>(Lifetime.Scoped);
            builder.Register<BaseState, SetUpState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<BattlePtPresenter>();
                entryPoints.Add<OrderPresenter>();
                entryPoints.Add<StatePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<BattleView>();
            builder.RegisterComponentInHierarchy<BattlePtView>();
            builder.RegisterComponentInHierarchy<OrderView>();
            builder.RegisterComponentInHierarchy<TableView>();
        }
    }
}