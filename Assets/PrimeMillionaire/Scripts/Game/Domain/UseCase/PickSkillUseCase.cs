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
        private readonly SkillRepository _skillRepository;

        public PickSkillUseCase(LevelEntity levelEntity, SkillRepository skillRepository)
        {
            _levelEntity = levelEntity;
            _skillRepository = skillRepository;
        }

        public async UniTask LotAsync(CancellationToken token)
        {
            var skills = _skillRepository.FindLotteryTargets(_levelEntity.currentValue);
            await UniTask.WhenAll(skills
                .Select((x, i) => Router.Default.PublishAsync(new PickSkillVO(i, x), token).AsUniTask())
            );
        }
    }
}