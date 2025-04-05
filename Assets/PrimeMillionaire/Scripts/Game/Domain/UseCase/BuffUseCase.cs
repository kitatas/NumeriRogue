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
        private readonly HoldSkillEntity _holdSkillEntity;
        private readonly BuffRepository _buffRepository;

        public BuffUseCase(BuffEntity buffEntity, HoldSkillEntity holdSkillEntity, BuffRepository buffRepository)
        {
            _buffEntity = buffEntity;
            _holdSkillEntity = holdSkillEntity;
            _buffRepository = buffRepository;
        }

        public async UniTask ActivateBuffAsync(CancellationToken token)
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
                }
            }

            _buffEntity.ApplyTotal();
        }
    }
}