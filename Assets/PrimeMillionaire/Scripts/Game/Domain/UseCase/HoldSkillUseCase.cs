using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HoldSkillUseCase
    {
        private readonly HoldSkillEntity _holdSkillEntity;

        public HoldSkillUseCase(HoldSkillEntity holdSkillEntity)
        {
            _holdSkillEntity = holdSkillEntity;
        }

        public async UniTaskVoid AddAsync(SkillVO skill, CancellationToken token)
        {
            _holdSkillEntity.Add(skill);
            await Router.Default.PublishAsync(_holdSkillEntity.ToVO(), token);
        }
    }
}