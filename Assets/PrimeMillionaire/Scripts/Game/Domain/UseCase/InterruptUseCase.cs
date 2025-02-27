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
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly SaveRepository _saveRepository;

        public InterruptUseCase(PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyCharacterEntity enemyCharacterEntity,
            EnemyParameterEntity enemyParameterEntity, SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _saveRepository = saveRepository;
        }

        public void Save()
        {
            var interrupt = new InterruptVO(
                playerCharacter: _playerCharacterEntity.type,
                playerParameter: _playerParameterEntity.ToVO(),
                enemyCharacter: _enemyCharacterEntity.type,
                enemyParameter: _enemyParameterEntity.ToVO()
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
                _playerParameterEntity.InitForInterrupt(interrupt.playerParameter);
                _enemyCharacterEntity.SetType(interrupt.enemyCharacter);
                _enemyParameterEntity.InitForInterrupt(interrupt.enemyParameter);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}