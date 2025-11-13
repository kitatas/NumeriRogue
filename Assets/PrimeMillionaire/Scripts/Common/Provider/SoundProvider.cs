using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Common.Provider
{
    public enum SoundType
    {
        None = 0,
        UnityAudio = 1,
        Cri = 2,
    }

    public sealed class SoundProvider
    {
        private readonly SoundType _type;

        public SoundProvider(SoundType type)
        {
            _type = type;
        }

        public ISoundUseCase ProvideUseCase(LoadingEntity loadingEntity, SaveRepository saveRepository,
            SoundRepository soundRepository)
        {
            return _type switch
            {
                SoundType.UnityAudio => new SoundUseCase(loadingEntity, saveRepository, soundRepository),
                SoundType.Cri => new CriSoundUseCase(loadingEntity, saveRepository),
                _ => throw new QuitExceptionVO(ExceptionConfig.FAILED_DEPENDENCY_RESOLUTION),
            };
        }

        public ISoundView ProvideView()
        {
            return _type switch
            {
                SoundType.UnityAudio => Object.FindFirstObjectByType<SoundView>(),
                SoundType.Cri => Object.FindFirstObjectByType<CriSoundView>(),
                _ => throw new QuitExceptionVO(ExceptionConfig.FAILED_DEPENDENCY_RESOLUTION),
            };
        }
    }
}