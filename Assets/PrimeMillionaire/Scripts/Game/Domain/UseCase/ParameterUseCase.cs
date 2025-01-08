using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;

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

        public void Init()
        {
            var playerParameter = _parameterRepository.Find(CharacterType.Andromeda);
            _playerParameterEntity.SetParameter(playerParameter);

            var enemyParameter = _parameterRepository.Find(CharacterType.Borealjuggernaut);
            _enemyParameterEntity.SetParameter(enemyParameter);
        }
    }
}