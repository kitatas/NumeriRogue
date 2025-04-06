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

        public BuffUseCase(BuffEntity buffEntity, DollarEntity dollarEntity, HoldSkillEntity holdSkillEntity,
            PlayerParameterEntity playerParameterEntity, BuffRepository buffRepository)
        {
            _buffEntity = buffEntity;
            _dollarEntity = dollarEntity;
            _holdSkillEntity = holdSkillEntity;
            _playerParameterEntity = playerParameterEntity;
            _buffRepository = buffRepository;
        }

        public async UniTask ActivateBuffAsync(Action update, CancellationToken token)
        {
            var activateBuffs = _holdSkillEntity.allTypes
                .Where(x => _buffEntity.HasAnyCurrent(x))
                .ToList();

            if (activateBuffs.Count > 0)
            {
                await UniTaskHelper.DelayAsync(UiConfig.TWEEN_DURATION, token);

                foreach (var type in activateBuffs)
                {
                    var fx = _buffRepository.FindFxObject(type);
                    await Router.Default.PublishAsync(new BuffVO(Side.Player, fx), token);

                    ApplySkillEffect(type);
                    update?.Invoke();
                }
            }

            _buffEntity.ApplyTotal();
        }

        private void ApplySkillEffect(SkillType type)
        {
            if (SkillConfig.DOLLAR_SKILLS.Any(x => x == type))
            {
                _dollarEntity.Add(_holdSkillEntity.GetTotalValue(type));
            }
            else if (SkillConfig.HEAL_SKILLS.Any(x => x == type))
            {
                _playerParameterEntity.Heal(_holdSkillEntity.GetTotalValue(type));
            }
        }
    }
}