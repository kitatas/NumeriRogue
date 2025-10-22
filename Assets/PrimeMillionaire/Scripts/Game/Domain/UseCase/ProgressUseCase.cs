using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class ProgressUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;

        public ProgressUseCase(PlayerCharacterEntity playerCharacterEntity, UserEntity userEntity,
            PlayFabRepository playFabRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask UpdateProgressAsync(ProgressStatus status, CancellationToken token)
        {
            var progress = _userEntity.progress;
            var type = _playerCharacterEntity.type;
            var characterProgress = progress.Find(type);
            if (characterProgress == null)
            {
                // 初挑戦であればレコードを作成
                progress.characterProgress.Add(new CharacterProgressVO(type, status));
            }
            else if (status == ProgressStatus.None || characterProgress.isClear)
            {
                // 失敗済み or クリア済みであれば更新不要
                return;
            }
            else
            {
                progress.characterProgress.Remove(characterProgress);
                progress.characterProgress.Add(new CharacterProgressVO(type, status));
            }

            await _playFabRepository.UpdateUserProgressAsync(progress, token);
        }
    }
}