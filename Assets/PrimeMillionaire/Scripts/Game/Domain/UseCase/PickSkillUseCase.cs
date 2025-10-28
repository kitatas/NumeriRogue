using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class PickSkillUseCase
    {
        private readonly LevelEntity _levelEntity;
        private readonly LotSkillEntity _lotSkillEntity;
        private readonly SkillRepository _skillRepository;

        public PickSkillUseCase(LevelEntity levelEntity, LotSkillEntity lotSkillEntity,
            SkillRepository skillRepository)
        {
            _levelEntity = levelEntity;
            _lotSkillEntity = lotSkillEntity;
            _skillRepository = skillRepository;
        }

        public async UniTask LotAsync(CancellationToken token)
        {
            var skills = _skillRepository.FindLotteryTargets(_levelEntity.currentValue);
            _lotSkillEntity.Set(skills);

            await UniTask.WhenAll(_lotSkillEntity.ToPickVOs()
                .Select(x => Router.Default.PublishAsync(x, token).AsUniTask())
            );

            _lotSkillEntity.Clear();
        }
    }
}