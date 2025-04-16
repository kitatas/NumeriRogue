using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BuffUseCase
    {
        private readonly BuffEntity _buffEntity;
        private readonly DollarEntity _dollarEntity;
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly BuffRepository _buffRepository;
        private readonly SkillRepository _skillRepository;

        public BuffUseCase(BuffEntity buffEntity, DollarEntity dollarEntity, HoldSkillEntity holdSkillEntity,
            PlayerParameterEntity playerParameterEntity, BuffRepository buffRepository, SkillRepository skillRepository)
        {
            _buffEntity = buffEntity;
            _dollarEntity = dollarEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerParameterEntity = playerParameterEntity;
            _buffRepository = buffRepository;
            _skillRepository = skillRepository;
        }

        public async UniTask ActivateBuffAsync(Action update, CancellationToken token)
        {
            var activateSkills = _holdSkillEntity.all
                .Where(x => _buffEntity.HasAnyCurrent(x.skillBase.type))
                .ToList();

            if (activateSkills.Count > 0)
            {
                await UniTaskHelper.DelayAsync(UiConfig.TWEEN_DURATION, token);

                foreach (var skill in activateSkills)
                {
                    var fx = _buffRepository.FindFxObject(skill.skillBase.type);
                    await Router.Default.PublishAsync(new BuffVO(Side.Player, fx), token);

                    ApplySkillEffect(skill);
                    update?.Invoke();
                }
            }

            _buffEntity.ApplyTotal();
        }

        private void ApplySkillEffect(SkillVO skill)
        {
            if (_skillRepository.IsExistTarget(skill.skillBase.target, SkillTarget.Dollar))
            {
                _dollarEntity.Add(skill.skillEffect.value);
            }

            if (_skillRepository.IsExistTarget(skill.skillBase.target, SkillTarget.Heal))
            {
                _playerParameterEntity.Heal(skill.skillEffect.value);
            }
        }
    }
}