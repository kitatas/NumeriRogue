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
        private readonly DollarEntity _dollarEntity;
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly LevelEntity _levelEntity;
        private readonly TurnEntity _turnEntity;
        private readonly SaveRepository _saveRepository;

        public InterruptUseCase(CommunityBattlePtEntity communityBattlePtEntity, DeckEntity deckEntity,
            DollarEntity dollarEntity, HoldSkillEntity holdSkillEntity, PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyCharacterEntity enemyCharacterEntity,
            EnemyCountEntity enemyCountEntity, EnemyParameterEntity enemyParameterEntity, LevelEntity levelEntity,
            TurnEntity turnEntity, SaveRepository saveRepository)
        {
            _communityBattlePtEntity = communityBattlePtEntity;
            _deckEntity = deckEntity;
            _dollarEntity = dollarEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _enemyCountEntity = enemyCountEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _levelEntity = levelEntity;
            _turnEntity = turnEntity;
            _saveRepository = saveRepository;
        }

        public void Save()
        {
            var interrupt = new InterruptVO(
                _playerParameterEntity.ToVO(),
                _enemyParameterEntity.ToVO(),
                _deckEntity.ToVO(),
                _holdSkillEntity.ToVO(),
                _dollarEntity.currentValue,
                _levelEntity.currentValue,
                _turnEntity.currentValue,
                _enemyCountEntity.currentValue,
                _communityBattlePtEntity.currentValue
            );

            _saveRepository.Save(interrupt);
        }

        public void Load()
        {
            if (_saveRepository.TryLoadInterrupt(out var interrupt))
            {
                _playerCharacterEntity.SetType(interrupt.player.type);
                _playerParameterEntity.InitForInterrupt(interrupt.player);
                _enemyCharacterEntity.SetType(interrupt.enemy.type);
                _enemyCountEntity.Set(interrupt.enemyCount);
                _enemyParameterEntity.InitForInterrupt(interrupt.enemy);
                _levelEntity.Set(interrupt.level);
                _turnEntity.Set(interrupt.turn);
                _deckEntity.Init(interrupt.deck);
                _communityBattlePtEntity.Set(interrupt.communityBattlePt);
                _dollarEntity.Set(interrupt.dollar);
                _holdSkillEntity.Init(interrupt.holdSkill);
            }
            else
            {
                throw new RebootExceptionVO(ExceptionConfig.FAILED_LOAD_INTERRUPT);
            }
        }

        public void Delete()
        {
            _saveRepository.DeleteInterrupt();
        }
    }
}