using System;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class InterruptUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly SaveRepository _saveRepository;

        public InterruptUseCase(PlayerCharacterEntity playerCharacterEntity, EnemyCharacterEntity enemyCharacterEntity,
            SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _saveRepository = saveRepository;
        }

        public void Save()
        {
            var interrupt = new InterruptVO(
                playerCharacter: _playerCharacterEntity.type,
                enemyCharacter: _enemyCharacterEntity.type
            );

            _saveRepository.Save(interrupt);
        }

        public void Load()
        {
            var saveData = _saveRepository.Load();
            if (saveData.HasInterrupt())
            {
                var interrupt = saveData.interrupt;
                _playerCharacterEntity.SetType(interrupt.playerCharacter);
                _enemyCharacterEntity.SetType(interrupt.enemyCharacter);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}