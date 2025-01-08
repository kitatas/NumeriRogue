using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class ParameterUseCase
    {
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly ParameterRepository _parameterRepository;

        public ParameterUseCase(PlayerParameterEntity playerParameterEntity, EnemyParameterEntity enemyParameterEntity,
            ParameterRepository parameterRepository)
        {
            _playerParameterEntity = playerParameterEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _parameterRepository = parameterRepository;
        }

        public async UniTask InitAsync(CancellationToken token)
        {
            var playerParameter = _parameterRepository.Find(CharacterType.Andromeda);
            _playerParameterEntity.Init(playerParameter);

            var enemyParameter = _parameterRepository.Find(CharacterType.Borealjuggernaut);
            _enemyParameterEntity.Init(enemyParameter);

            await (
                Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token)
            );
        }
    }
}