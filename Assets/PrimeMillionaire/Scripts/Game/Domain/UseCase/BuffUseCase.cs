using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BuffUseCase
    {
        private readonly BuffEntity _buffEntity;
        private readonly HoldSkillEntity _holdSkillEntity;

        public BuffUseCase(BuffEntity buffEntity, HoldSkillEntity holdSkillEntity)
        {
            _buffEntity = buffEntity;
            _holdSkillEntity = holdSkillEntity;
        }

        public async UniTask ActivateBuffAsync(CancellationToken token)
        {
            var isActivateBuff = _holdSkillEntity.allTypes
                .Select(x => _buffEntity.HasAnyCurrent(x))
                .Any(x => x);
            await Router.Default.PublishAsync(new BuffVO(isActivateBuff), token);

            _buffEntity.ApplyTotal();
        }
    }
}