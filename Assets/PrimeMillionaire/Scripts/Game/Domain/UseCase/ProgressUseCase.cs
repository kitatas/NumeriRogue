using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class ProgressUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly SaveRepository _saveRepository;

        public ProgressUseCase(PlayerCharacterEntity playerCharacterEntity, SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _saveRepository = saveRepository;
        }

        public void UpdateProgress()
        {
            if (_saveRepository.TryLoadProgress(out var progress))
            {
                var characterNo = _playerCharacterEntity.typeToInt;
                var clear = progress.clears.Find(x => x.characterNo == characterNo);
 
                if (clear == null)
                {
                    progress.clears.Add(new ClearVO(characterNo, true));
                }
                else
                {
                    clear.isClear = true;
                }

                _saveRepository.Save(progress);
            }
            else
            {
                throw new RebootExceptionVO(ExceptionConfig.FAILED_LOAD_PROGRESS);
            }
        }
    }
}