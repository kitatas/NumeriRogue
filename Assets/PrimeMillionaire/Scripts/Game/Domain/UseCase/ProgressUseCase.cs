using FastEnumUtility;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using UnityEngine;

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
                progress.characterNo = Mathf.Max(progress.characterNo, _playerCharacterEntity.typeToInt);
                _saveRepository.Save(progress);
            }
        }
    }
}