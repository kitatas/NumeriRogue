using PrimeMillionaire.Game.Data.DataStore;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.Presenter;
using PrimeMillionaire.Game.Presentation.State;
using PrimeMillionaire.Game.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Installer
{
    public sealed class GameInstaller : LifetimeScope
    {
        [SerializeField] private TextAsset memoryFile = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<MemoryDatabase>(new MemoryDatabase(memoryFile.bytes));

            // Entity
            builder.Register<DeckEntity>(Lifetime.Scoped);
            builder.Register<EnemyBattlePtEntity>(Lifetime.Scoped);
            builder.Register<EnemyHandEntity>(Lifetime.Scoped);
            builder.Register<EnemyParameterEntity>(Lifetime.Scoped);
            builder.Register<PlayerBattlePtEntity>(Lifetime.Scoped);
            builder.Register<PlayerHandEntity>(Lifetime.Scoped);
            builder.Register<PlayerParameterEntity>(Lifetime.Scoped);

            // Repository
            builder.Register<CardRepository>(Lifetime.Scoped);
            builder.Register<CharacterRepository>(Lifetime.Scoped);
            builder.Register<ParameterRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BattlePtUseCase>(Lifetime.Scoped);
            builder.Register<CharacterUseCase>(Lifetime.Scoped);
            builder.Register<DealUseCase>(Lifetime.Scoped);
            builder.Register<HandUseCase>(Lifetime.Scoped);
            builder.Register<OrderUseCase>(Lifetime.Scoped);
            builder.Register<ParameterUseCase>(Lifetime.Scoped);
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
                entryPoints.Add<ParameterPresenter>();
                entryPoints.Add<StatePresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<BattleView>();
            builder.RegisterComponentInHierarchy<BattlePtView>();
            builder.RegisterComponentInHierarchy<OrderView>();
            builder.RegisterComponentInHierarchy<PlayerParameterView>();
            builder.RegisterComponentInHierarchy<TableView>();
        }
    }
}