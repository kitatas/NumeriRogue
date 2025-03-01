using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class PickSkillUseCase
    {
        private readonly EnemyCountEntity _enemyCountEntity;
        private readonly SkillRepository _skillRepository;

        public PickSkillUseCase(EnemyCountEntity enemyCountEntity, SkillRepository skillRepository)
        {
            _enemyCountEntity = enemyCountEntity;
            _skillRepository = skillRepository;
        }

        public async UniTask LotAsync(CancellationToken token)
        {
            var skills = _skillRepository.FindLotteryTargets(_enemyCountEntity.currentValue);
            await UniTask.WhenAll(skills
                .Select((x, i) => Router.Default.PublishAsync(new PickSkillVO(i, x), token).AsUniTask())
            );

            await ActivateModalAsync(true, token);
            await ActivateModalAsync(false, token);
        }

        public async UniTask ActivateModalAsync(bool value, CancellationToken token)
        {
            await Router.Default.PublishAsync(new ModalVO(ModalType.PickSkill, value), token);
        }
    }
}