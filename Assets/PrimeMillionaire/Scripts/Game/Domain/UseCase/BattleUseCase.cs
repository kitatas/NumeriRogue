using System.Linq;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UnityEngine;

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

        public void ApplyDamage(Side attacker)
        {
            if (attacker == Side.Player) _enemyParameterEntity.Damage(GetEnemyDamage());
            else if (attacker == Side.Enemy) _playerParameterEntity.Damage(GetPlayerDamage());
            else throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE);

            _buffEntity.Clear();
        }

        private int GetEnemyDamage()
        {
            var rate = GetSkillRate(SkillTarget.Atk) + 1.0f;
            var atk = _playerBattlePtEntity.currentValue + Mathf.CeilToInt(_playerParameterEntity.atk * rate);
            var def = _enemyBattlePtEntity.currentValue + _enemyParameterEntity.def;
            return CalcDamage(atk, def);
        }

        private int GetPlayerDamage()
        {
            var rate = GetSkillRate(SkillTarget.Def) + 1.0f;
            var atk = _enemyBattlePtEntity.currentValue + _enemyParameterEntity.atk;
            var def = _playerBattlePtEntity.currentValue + Mathf.CeilToInt(_playerParameterEntity.def * rate);
            return CalcDamage(atk, def);
        }

        private float GetSkillRate(SkillTarget target)
        {
            return _skillRepository.FindsSkillType(target)
                .Sum(x => _holdSkillEntity.GetTotalRate(x) * _buffEntity.GetTotalCount(x));
        }

        private static int CalcDamage(float atk, float def)
        {
            return Mathf.CeilToInt(atk / def * 100 * Random.Range(0.95f, 1.05f));
        }
    }
}