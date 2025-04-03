using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattleUseCase
    {
        private readonly BuffEntity _buffEntity;
        private readonly EnemyBattlePtEntity _enemyBattlePtEntity;
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerBattlePtEntity _playerBattlePtEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;

        public BattleUseCase(BuffEntity buffEntity, EnemyBattlePtEntity enemyBattlePtEntity,
            EnemyParameterEntity enemyParameterEntity, HoldSkillEntity holdSkillEntity,
            PlayerBattlePtEntity playerBattlePtEntity, PlayerParameterEntity playerParameterEntity)
        {
            _buffEntity = buffEntity;
            _enemyBattlePtEntity = enemyBattlePtEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerBattlePtEntity = playerBattlePtEntity;
            _playerParameterEntity = playerParameterEntity;
        }

        public bool IsDestroy()
        {
            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                return _enemyParameterEntity.currentHpWithAdditional <= GetEnemyDamage();
            }
            else
            {
                return _playerParameterEntity.currentHpWithAdditional <= GetPlayerDamage();
            }
        }

        public async UniTask ExecBattleAsync(CancellationToken token)
        {
            // Damage Animation との調整
            await UniTaskHelper.DelayAsync(0.25f, token);

            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                _enemyParameterEntity.Damage(GetEnemyDamage());
                await Router.Default.PublishAsync(_enemyParameterEntity.ToVO(), token);
            }
            else
            {
                _playerParameterEntity.Damage(GetPlayerDamage());
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }

            _buffEntity.Clear();
        }

        private int GetEnemyDamage()
        {
            var rate = GetAtkSkillRate() + 1.0f;
            var atk = _playerBattlePtEntity.currentValue + Mathf.CeilToInt(_playerParameterEntity.atk * rate);
            var def = _enemyBattlePtEntity.currentValue + _enemyParameterEntity.def;
            return CalcDamage(atk, def);
        }

        private int GetPlayerDamage()
        {
            var rate = GetDefSkillRate() + 1.0f;
            var atk = _enemyBattlePtEntity.currentValue + _enemyParameterEntity.atk;
            var def = _playerBattlePtEntity.currentValue + Mathf.CeilToInt(_playerParameterEntity.def * rate);
            return CalcDamage(atk, def);
        }

        private float GetAtkSkillRate()
        {
            return GetSkillRate(SkillType.OddAtk) +
                   GetSkillRate(SkillType.EvenAtk);
        }

        private float GetDefSkillRate()
        {
            return GetSkillRate(SkillType.OddDef) +
                   GetSkillRate(SkillType.EvenDef);
        }

        private float GetSkillRate(SkillType type)
        {
            return _holdSkillEntity.GetTotalRate(type) * _buffEntity.GetTotalCount(type);
        }

        private static int CalcDamage(float atk, float def)
        {
            return Mathf.CeilToInt(atk / def * 100 * Random.Range(0.95f, 1.05f));
        }
    }
}