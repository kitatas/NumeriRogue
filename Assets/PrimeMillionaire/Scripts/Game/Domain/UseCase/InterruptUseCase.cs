using System;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class InterruptUseCase
    {
        private readonly CommunityBattlePtEntity _communityBattlePtEntity;
        private readonly DeckEntity _deckEntity;
        private readonly DollarEntity  _dollarEntity;
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly TurnEntity _turnEntity;
        private readonly SaveRepository _saveRepository;

        public InterruptUseCase(CommunityBattlePtEntity communityBattlePtEntity, DeckEntity deckEntity,
            DollarEntity dollarEntity, HoldSkillEntity holdSkillEntity, PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyCharacterEntity enemyCharacterEntity,
            EnemyParameterEntity enemyParameterEntity, EnemyCountEntity enemyCountEntity, TurnEntity turnEntity,
            SaveRepository saveRepository)
        {
            _communityBattlePtEntity = communityBattlePtEntity;
            _deckEntity = deckEntity;
            _dollarEntity = dollarEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _enemyCountEntity = enemyCountEntity;
            _turnEntity = turnEntity;
            _saveRepository = saveRepository;
        }

        public void Save()
        {
            var interrupt = new InterruptVO(
                playerCharacter: _playerCharacterEntity.type,
                playerParameter: _playerParameterEntity.ToVO(),
                enemyCharacter: _enemyCharacterEntity.type,
                enemyParameter: _enemyParameterEntity.ToVO(),
                enemyCount: _enemyCountEntity.currentValue,
                turn: _turnEntity.currentValue,
                deck: _deckEntity.ToVO(),
                communityBattlePt: _communityBattlePtEntity.currentValue,
                dollar: _dollarEntity.currentValue,
                holdSkill: _holdSkillEntity.ToVO()
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
                _enemyCountEntity.Set(interrupt.enemyCount);
                _turnEntity.Set(interrupt.turn);
                _deckEntity.Init(interrupt.deck);
                _communityBattlePtEntity.Set(interrupt.communityBattlePt);
                _dollarEntity.Set(interrupt.dollar);
                _holdSkillEntity.Init(interrupt.holdSkill);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}