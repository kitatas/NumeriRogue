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
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly ParameterRepository _parameterRepository;
        private readonly LevelRepository _levelRepository;

        public ParameterUseCase(PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyParameterEntity enemyParameterEntity,
            EnemyCountEntity enemyCountEntity, ParameterRepository parameterRepository, LevelRepository levelRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _enemyCountEntity = enemyCountEntity;
            _parameterRepository = parameterRepository;
            _levelRepository = levelRepository;
        }

        public async UniTask InitPlayerParamAsync(CancellationToken token)
        {
            var parameter = _parameterRepository.Find(_playerCharacterEntity.type);
            _playerParameterEntity.Init(parameter);

            await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
        }

        public async UniTask InitEnemyParamAsync(CancellationToken token)
        {
            // TODO: CharacterTypeのベタ書き
            var parameter = _parameterRepository.Find(CharacterType.Borealjuggernaut);
            var level = _levelRepository.FindClosest(_enemyCountEntity.currentValue);
            _enemyParameterEntity.Init(parameter, level);

            await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
        }
    }
}