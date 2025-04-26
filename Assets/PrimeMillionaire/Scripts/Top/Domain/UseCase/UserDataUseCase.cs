using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class UserDataUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly SaveRepository _saveRepository;

        public UserDataUseCase(PlayerCharacterEntity playerCharacterEntity, SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _saveRepository = saveRepository;
        }

        public void Delete()
        {
            _playerCharacterEntity.Reset();
            _saveRepository.Delete();
        }
    }
}