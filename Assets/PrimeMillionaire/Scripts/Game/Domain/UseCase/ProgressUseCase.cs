using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class ProgressUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly UserEntity _userEntity;

        public ProgressUseCase(PlayerCharacterEntity playerCharacterEntity, UserEntity userEntity)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _userEntity = userEntity;
        }

        public void UpdateProgress(ProgressStatus status)
        {
            var progress = _userEntity.progress;
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
                progress.characterProgress.Remove(characterProgress);
                progress.characterProgress.Add(new CharacterProgressVO(type, status));
            }

            // TODO: Send PlayFab
        }
    }
}