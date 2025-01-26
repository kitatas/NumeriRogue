using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UnityEngine;
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
            // TODO: master定義の最高levelベタ書き
            var wrapLevel = Mathf.Min(3, _levelEntity.currentValue);
            var skills = _skillRepository.FindLotteryTargets(wrapLevel);

            await UniTask.WhenAll(skills
                .Select((x, i) => Router.Default.PublishAsync(new PickSkillVO(i, x), token).AsUniTask())
            );

            await ActivateModalAsync(true, token);
        }

        public async UniTask ActivateModalAsync(bool value, CancellationToken token)
        {
            await Router.Default.PublishAsync(new ModalVO(ModalType.PickSkill, value), token);
        }
    }
}