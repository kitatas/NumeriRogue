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
                saveData.uid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                _saveRepository.Save(saveData);
            }

            return true;
        }
    }
}