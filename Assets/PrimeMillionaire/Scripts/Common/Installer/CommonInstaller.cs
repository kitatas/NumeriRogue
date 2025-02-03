using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.Presenter;
using PrimeMillionaire.Common.Presentation.View;
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

            // Repository
            builder.Register<CharacterRepository>(Lifetime.Scoped);
            builder.Register<ParameterRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<ScenePresenter>();
            });

            // View
            builder.RegisterInstance<TransitionView>(FindFirstObjectByType<TransitionView>());
        }
    }
}