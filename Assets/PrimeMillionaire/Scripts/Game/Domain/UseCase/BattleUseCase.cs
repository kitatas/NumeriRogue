using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattleUseCase
    {
        private readonly PlayerBattlePtEntity _playerBattlePtEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyBattlePtEntity _enemyBattlePtEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;

        public BattleUseCase(PlayerBattlePtEntity playerBattlePtEntity, PlayerParameterEntity playerParameterEntity,
            EnemyBattlePtEntity enemyBattlePtEntity, EnemyParameterEntity enemyParameterEntity)
        {
            _playerBattlePtEntity = playerBattlePtEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyBattlePtEntity = enemyBattlePtEntity;
            _enemyParameterEntity = enemyParameterEntity;
        }

        public bool IsDestroy()
        {
            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                var atk = _playerParameterEntity.atk * _playerBattlePtEntity.currentValue;
                var def = _enemyParameterEntity.def * _enemyBattlePtEntity.currentValue;
                return _enemyParameterEntity.currentHp <= atk - def;
            }
            else
            {
                var atk = _enemyParameterEntity.atk * _enemyBattlePtEntity.currentValue;
                var def = _playerParameterEntity.def * _playerBattlePtEntity.currentValue;
                return _playerParameterEntity.currentHp <= atk - def;
            }
        }

        public async UniTask ExecBattleAsync(CancellationToken token)
        {
            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                var atk = _playerParameterEntity.atk * _playerBattlePtEntity.currentValue;
                var def = _enemyParameterEntity.def * _enemyBattlePtEntity.currentValue;
                _enemyParameterEntity.Damage(atk - def);
                await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
            }
            else
            {
                var atk = _enemyParameterEntity.atk * _enemyBattlePtEntity.currentValue;
                var def = _playerParameterEntity.def * _playerBattlePtEntity.currentValue;
                _playerParameterEntity.Damage(atk - def);
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }
        }
    }
}