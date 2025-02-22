using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class ParameterUseCase
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyCharacterEntity _enemyCharacterEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly LevelRepository _levelRepository;

        public ParameterUseCase(PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyCharacterEntity enemyCharacterEntity,
            EnemyParameterEntity enemyParameterEntity, EnemyCountEntity enemyCountEntity,
            CharacterRepository characterRepository, LevelRepository levelRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _enemyCountEntity = enemyCountEntity;
            _characterRepository = characterRepository;
            _levelRepository = levelRepository;
        }

        public async UniTask InitPlayerParamAsync(CancellationToken token)
        {
            var character = _characterRepository.Find(_playerCharacterEntity.type);
            _playerParameterEntity.Init(character.parameter);

            await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
        }

        public async UniTask InitEnemyParamAsync(CancellationToken token)
        {
            var character = _characterRepository.Find(_enemyCharacterEntity.type);
            var level = _levelRepository.FindClosest(_enemyCountEntity.currentValue);
            _enemyParameterEntity.Init(character.parameter, level);

            await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
        }
    }
}