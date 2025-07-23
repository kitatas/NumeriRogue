using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using PrimeMillionaire.Common.Utility;
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
        private readonly LevelEntity _levelEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly LevelRepository _levelRepository;

        public ParameterUseCase(PlayerCharacterEntity playerCharacterEntity,
            PlayerParameterEntity playerParameterEntity, EnemyCharacterEntity enemyCharacterEntity,
            EnemyParameterEntity enemyParameterEntity, LevelEntity levelEntity,
            CharacterRepository characterRepository, LevelRepository levelRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyCharacterEntity = enemyCharacterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _levelEntity = levelEntity;
            _characterRepository = characterRepository;
            _levelRepository = levelRepository;
        }

        public async UniTask InitPlayerParamAsync(CancellationToken token)
        {
            var character = _characterRepository.Find(_playerCharacterEntity.type);
            _playerParameterEntity.Init(character.parameter);

            await PublishPlayerParamAsync(token);
        }

        public async UniTask PublishPlayerParamAsync(CancellationToken token)
        {
            await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
        }

        public async UniTask InitEnemyParamAsync(CancellationToken token)
        {
            var character = _characterRepository.Find(_enemyCharacterEntity.type);
            var level = _levelRepository.FindClosest(_levelEntity.currentValue);
            _enemyParameterEntity.Init(character.parameter, level);

            await PublishEnemyParamAsync(token);
        }

        public async UniTask PublishEnemyParamAsync(CancellationToken token)
        {
            await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
        }

        public bool IsDestroy(Side attacker)
        {
            return attacker switch
            {
                Side.Player => _enemyParameterEntity.isDead,
                Side.Enemy => _playerParameterEntity.isDead,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public async UniTask ApplyDamageAsync(CancellationToken token)
        {
            // Damage Animation との調整
            await UniTaskHelper.DelayAsync(0.25f, token);

            await (
                PublishPlayerParamAsync(token),
                PublishEnemyParamAsync(token)
            );
        }
    }
}