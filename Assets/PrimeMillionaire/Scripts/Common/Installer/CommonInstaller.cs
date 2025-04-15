using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.Presenter;
using PrimeMillionaire.Common.Presentation.View;
using PrimeMillionaire.Common.Presentation.View.Modal;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Installer
{
    public sealed class CommonInstaller : LifetimeScope
    {
        [SerializeField] private TextAsset memoryFile = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<MemoryDatabase>(new MemoryDatabase(memoryFile.bytes));

            // Entity
            builder.Register<PlayerCharacterEntity>(Lifetime.Singleton);
            builder.Register<RetryCountEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<CharacterRepository>(Lifetime.Singleton);
            builder.Register<CharacterStageRepository>(Lifetime.Singleton);
            builder.Register<SaveRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<ExceptionUseCase>(Lifetime.Singleton);
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<ExceptionPresenter>();
                entryPoints.Add<ScenePresenter>();
            });

            // View
            builder.RegisterInstance<ExceptionModalView>(FindFirstObjectByType<ExceptionModalView>());
            builder.RegisterInstance<TransitionView>(FindFirstObjectByType<TransitionView>());
        }
    }
}