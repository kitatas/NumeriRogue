using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class UserDataUseCase
    {
        private readonly SaveRepository _saveRepository;

        public UserDataUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
        }

        public void Delete()
        {
            _saveRepository.Delete();
        }
    }
}