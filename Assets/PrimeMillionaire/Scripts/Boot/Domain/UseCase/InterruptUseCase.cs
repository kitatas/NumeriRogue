using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class InterruptUseCase
    {
        private readonly SaveRepository _saveRepository;

        public InterruptUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
        }

        public bool HasInterrupt()
        {
            return _saveRepository.TryLoadInterrupt(out _);
        }

        public void Delete()
        {
            _saveRepository.DeleteInterrupt();
        }
    }
}