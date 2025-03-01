using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
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
        public bool hasEmpty => _holdSkillEntity.count < SkillConfig.HOLD_NUM;

        public async UniTaskVoid AddAsync(SkillVO skill, CancellationToken token)
        {
            _holdSkillEntity.Add(skill);
            _holdCount.Value = _holdSkillEntity.count;
            await (
                ApplyViewAsync(token),
                ApplyParameterAsync(skill.type, token)
            );
        }

        public async UniTaskVoid RemoveAsync(SkillVO skill, CancellationToken token)
        {
            _holdSkillEntity.Remove(skill);
            _holdCount.Value = _holdSkillEntity.count;
            await (
                ApplyViewAsync(token),
                ApplyParameterAsync(skill.type, token)
            );
        }

        public async UniTask ApplyViewAsync(CancellationToken token)
        {
            await Router.Default.PublishAsync(_holdSkillEntity.ToVO(), token);
        }

        public async UniTask ApplyParameterAsync(SkillType type, CancellationToken token)
        {
            if (type == SkillType.HpUp)
            {
                var rate = _holdSkillEntity.GetTotalRate(type);
                _playerParameterEntity.SetAdditionalHp(rate);
                await Router.Default.PublishAsync(_playerParameterEntity.ToVO(), token);
            }
        }
    }
}