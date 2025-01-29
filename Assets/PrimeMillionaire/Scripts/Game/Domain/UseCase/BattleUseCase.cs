using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattleUseCase
    {
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerBattlePtEntity _playerBattlePtEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly EnemyBattlePtEntity _enemyBattlePtEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;

        public BattleUseCase(HoldSkillEntity holdSkillEntity,
            PlayerBattlePtEntity playerBattlePtEntity, PlayerParameterEntity playerParameterEntity,
            EnemyBattlePtEntity enemyBattlePtEntity, EnemyParameterEntity enemyParameterEntity)
        {
            _holdSkillEntity = holdSkillEntity;
            _playerBattlePtEntity = playerBattlePtEntity;
            _playerParameterEntity = playerParameterEntity;
            _enemyBattlePtEntity = enemyBattlePtEntity;
            _enemyParameterEntity = enemyParameterEntity;
        }

        public bool IsDestroy()
        {
            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                var atk = GetPlayerAtk() * _playerBattlePtEntity.currentValue;
                var def = _enemyParameterEntity.def * _enemyBattlePtEntity.currentValue;
                return _enemyParameterEntity.currentHp <= atk - def;
            }
            else
            {
                var atk = _enemyParameterEntity.atk * _enemyBattlePtEntity.currentValue;
                var def = GetPlayerDef() * _playerBattlePtEntity.currentValue;
                return _playerParameterEntity.currentHp <= atk - def;
            }
        }

        public async UniTask ExecBattleAsync(CancellationToken token)
        {
            // Damage Animation との調整
            await UniTaskHelper.DelayAsync(0.25f, token);

            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                var atk = GetPlayerAtk() * _playerBattlePtEntity.currentValue;
                var def = _enemyParameterEntity.def * _enemyBattlePtEntity.currentValue;
                _enemyParameterEntity.Damage(atk - def);
                await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
            }
            else
            {
                var atk = _enemyParameterEntity.atk * _enemyBattlePtEntity.currentValue;
                var def = GetPlayerDef() * _playerBattlePtEntity.currentValue;
                _playerParameterEntity.Damage(atk - def);
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }
        }

        private int GetPlayerAtk()
        {
            var rate = _holdSkillEntity.GetTotalRate(SkillType.AtkUp) + 1.0f;
            return Mathf.CeilToInt(_playerParameterEntity.atk * rate);
        }

        private int GetPlayerDef()
        {
            var rate = _holdSkillEntity.GetTotalRate(SkillType.DefUp) + 1.0f;
            return Mathf.CeilToInt(_playerParameterEntity.def * rate);
        }
    }
}