using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.Presenter;
using PrimeMillionaire.Common.Presentation.View;
using PrimeMillionaire.Common.Presentation.View.Modal;
using PrimeMillionaire.Common.Provider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Installer
{
    public sealed class CommonInstaller : LifetimeScope
    {
        [SerializeField] private ProviderSettings providerSettings = default;
        [SerializeField] private TextAsset memoryFile = default;
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Provider
            var soundProvider = new SoundProvider(providerSettings.sound);

            // DataStore
            builder.RegisterInstance<MemoryDatabase>(new MemoryDatabase(memoryFile.bytes));
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Entity
            builder.Register<PlayerCharacterEntity>(Lifetime.Singleton);
            builder.Register<RetryCountEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<CharacterRepository>(Lifetime.Singleton);
            builder.Register<CharacterStageRepository>(Lifetime.Singleton);
            builder.Register<SaveRepository>(Lifetime.Singleton);
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<ExceptionUseCase>(Lifetime.Singleton);
            builder.Register<LoadingUseCase>(Lifetime.Singleton);
            builder.Register<SceneUseCase>(Lifetime.Singleton);
            builder.Register<ISoundUseCase>(x =>
            {
                var saveRepository = x.Resolve<SaveRepository>();
                var soundRepository = x.Resolve<SoundRepository>();
                return soundProvider.ProvideUseCase(saveRepository, soundRepository);
            }, Lifetime.Singleton);

            // Presenter
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<ExceptionPresenter>();
                entryPoints.Add<LoadingPresenter>();
                entryPoints.Add<ScenePresenter>();
                entryPoints.Add<SoundPresenter>();
            });

            // View
            builder.RegisterInstance<ExceptionModalView>(FindFirstObjectByType<ExceptionModalView>());
            builder.RegisterInstance<LoadingView>(FindFirstObjectByType<LoadingView>());
            builder.RegisterInstance<ISoundView>(soundProvider.ProvideView());
            builder.RegisterInstance<TransitionView>(FindFirstObjectByType<TransitionView>());
        }
    }
}