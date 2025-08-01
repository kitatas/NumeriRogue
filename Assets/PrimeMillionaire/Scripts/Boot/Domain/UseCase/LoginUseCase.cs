using System;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
        }

        public bool Login()
        {
            var saveData = _saveRepository.Load();
            if (saveData.IsEmptyUid())
            {
                saveData.uid = Ulid.NewUlid().ToString();
                _saveRepository.Save(saveData);
            }

            return true;
        }
    }
}