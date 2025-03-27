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

        public void UpdateProgress(ProgressStatus status)
        {
            if (_saveRepository.TryLoadProgress(out var progress))
            {
                var type = _playerCharacterEntity.type;
                var characterProgress = progress.Find(type);
                if (characterProgress == null)
                {
                    progress.characterProgress.Add(new CharacterProgressVO(type, status));
                }
                else if (characterProgress.isClear)
                {
                    // クリア済みであれば更新不要
                    return;
                }
                else
                {
                    characterProgress.status = status;
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