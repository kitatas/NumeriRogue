using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using R3;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HoldSkillUseCase
    {
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;
        private readonly ReactiveProperty<int> _holdCount;

        public HoldSkillUseCase(HoldSkillEntity holdSkillEntity, PlayerParameterEntity playerParameterEntity)
        {
            _holdSkillEntity = holdSkillEntity;
            _playerParameterEntity = playerParameterEntity;
            _holdCount = new ReactiveProperty<int>(_holdSkillEntity.count);
        }

        public Observable<bool> isFull => _holdCount.Select(x => x == SkillConfig.HOLD_NUM);

        public async UniTaskVoid AddAsync(SkillVO skill, CancellationToken token)
        {
            _holdSkillEntity.Add(skill);
            _holdCount.Value = _holdSkillEntity.count;
            await ApplyViewAsync(token);

            var rate = _holdSkillEntity.GetTotalRate(skill.type);
            if (skill.type == SkillType.HpUp)
            {
                _playerParameterEntity.SetAdditionalHp(rate);
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }
        }

        public async UniTaskVoid RemoveAsync(SkillVO skill, CancellationToken token)
        {
            _holdSkillEntity.Remove(skill);
            _holdCount.Value = _holdSkillEntity.count;
            await ApplyViewAsync(token);

            var rate = _holdSkillEntity.GetTotalRate(skill.type);
            if (skill.type == SkillType.HpUp)
            {
                _playerParameterEntity.SetAdditionalHp(rate);
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }
        }

        public async UniTask ApplyViewAsync(CancellationToken token)
        {
            await Router.Default.PublishAsync(_holdSkillEntity.ToVO(), token);
        }
    }
}