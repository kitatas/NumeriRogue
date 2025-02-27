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
            builder.Register<BonusEntity>(Lifetime.Scoped);
            builder.Register<CommunityBattlePtEntity>(Lifetime.Scoped);
            builder.Register<DeckEntity>(Lifetime.Scoped);
            builder.Register<DollarEntity>(Lifetime.Scoped);
            builder.Register<EnemyBattlePtEntity>(Lifetime.Scoped);
            builder.Register<EnemyCharacterEntity>(Lifetime.Scoped);
            builder.Register<EnemyCountEntity>(Lifetime.Scoped);
            builder.Register<EnemyHandEntity>(Lifetime.Scoped);
            builder.Register<EnemyParameterEntity>(Lifetime.Scoped);
            builder.Register<HoldSkillEntity>(Lifetime.Scoped);
            builder.Register<LevelEntity>(Lifetime.Scoped);
            builder.Register<PlayerBattlePtEntity>(Lifetime.Scoped);
            builder.Register<PlayerHandEntity>(Lifetime.Scoped);
            builder.Register<PlayerParameterEntity>(Lifetime.Scoped);
            builder.Register<TurnEntity>(Lifetime.Scoped);

            // Repository
            builder.Register<DropRepository>(Lifetime.Scoped);
            builder.Register<LevelRepository>(Lifetime.Scoped);
            builder.Register<PrimeNumberRepository>(Lifetime.Scoped);
            builder.Register<SkillRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BattlePtUseCase>(Lifetime.Scoped);
            builder.Register<BattleUseCase>(Lifetime.Scoped);
            builder.Register<CharacterUseCase>(Lifetime.Scoped);
            builder.Register<DealUseCase>(Lifetime.Scoped);
            builder.Register<DollarUseCase>(Lifetime.Scoped);
            builder.Register<DropUseCase>(Lifetime.Scoped);
            builder.Register<EnemyCountUseCase>(Lifetime.Scoped);
            builder.Register<HandUseCase>(Lifetime.Scoped);
            builder.Register<HoldSkillUseCase>(Lifetime.Scoped);
            builder.Register<InterruptUseCase>(Lifetime.Scoped);
            builder.Register<OrderUseCase>(Lifetime.Scoped);
            builder.Register<ParameterUseCase>(Lifetime.Scoped);
            builder.Register<PickSkillUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<TurnUseCase>(Lifetime.Scoped);

            // State
            builder.Register<BaseState, BattleState>(Lifetime.Scoped);
            builder.Register<BaseState, ClearState>(Lifetime.Scoped);
            builder.Register<BaseState, DealState>(Lifetime.Scoped);
            builder.Register<BaseState, FailState>(Lifetime.Scoped);
            builder.Register<BaseState, InitState>(Lifetime.Scoped);
            builder.Register<BaseState, LoadState>(Lifetime.Scoped);
            builder.Register<BaseState, OrderState>(Lifetime.Scoped);
            builder.Register<BaseState, PickState>(Lifetime.Scoped);
            builder.Register<BaseState, RestartState>(Lifetime.Scoped);
            builder.Register<BaseState, SetUpState>(Lifetime.Scoped);

            // Presenter
            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<BattlePtPresenter>();
                entryPoints.Add<DollarPresenter>();
                entryPoints.Add<EnemyCountPresenter>();
                entryPoints.Add<HoldSkillPresenter>();
                entryPoints.Add<OrderPresenter>();
                entryPoints.Add<ParameterPresenter>();
                entryPoints.Add<PickSkillPresenter>();
                entryPoints.Add<StatePresenter>();
                entryPoints.Add<TurnPresenter>();
            });

            // View
            builder.RegisterComponentInHierarchy<BattleView>();
            builder.RegisterComponentInHierarchy<BattlePtView>();
            builder.RegisterComponentInHierarchy<ClearView>();
            builder.RegisterComponentInHierarchy<DollarView>();
            builder.RegisterComponentInHierarchy<EnemyCountView>();
            builder.RegisterComponentInHierarchy<EnemyParameterView>();
            builder.RegisterComponentInHierarchy<FailView>();
            builder.RegisterComponentInHierarchy<HoldSkillView>();
            builder.RegisterComponentInHierarchy<OrderView>();
            builder.RegisterComponentInHierarchy<PickSkillView>();
            builder.RegisterComponentInHierarchy<PlayerParameterView>();
            builder.RegisterComponentInHierarchy<TableView>();
            builder.RegisterComponentInHierarchy<TurnView>();
        }
    }
}