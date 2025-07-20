using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
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
        private readonly SkillRepository _skillRepository;

        public BattleUseCase(BuffEntity buffEntity, EnemyBattlePtEntity enemyBattlePtEntity,
            EnemyParameterEntity enemyParameterEntity, HoldSkillEntity holdSkillEntity,
            PlayerBattlePtEntity playerBattlePtEntity, PlayerParameterEntity playerParameterEntity,
            SkillRepository skillRepository)
        {
            _buffEntity = buffEntity;
            _enemyBattlePtEntity = enemyBattlePtEntity;
            _enemyParameterEntity = enemyParameterEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerBattlePtEntity = playerBattlePtEntity;
            _playerParameterEntity = playerParameterEntity;
            _skillRepository = skillRepository;
        }

        public bool IsDestroy()
        {
            if (_playerBattlePtEntity.currentValue >= _enemyBattlePtEntity.currentValue)
            {
                return _enemyParameterEntity.currentHp <= GetEnemyDamage();
            }
            else
            {
                return _playerParameterEntity.currentHp <= GetPlayerDamage();
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
            return _skillRepository.FindsSkillType(SkillTarget.Atk)
                .Sum(GetSkillRate);
        }

        private float GetDefSkillRate()
        {
            return _skillRepository.FindsSkillType(SkillTarget.Def)
                .Sum(GetSkillRate);
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