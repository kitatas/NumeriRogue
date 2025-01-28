using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class PickSkillPresenter : IStartable
    {
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly PickSkillView _pickSkillView;

        public PickSkillPresenter(HoldSkillUseCase holdSkillUseCase, PickSkillView pickSkillView)
        {
            _holdSkillUseCase = holdSkillUseCase;
            _pickSkillView = pickSkillView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<PickSkillVO>(async (x, context) =>
                {
                    await _pickSkillView.RenderAsync(x, context.CancellationToken);
                })
                .AddTo(_pickSkillView);

            Router.Default
                .SubscribeAwait<ModalVO>(async (x, context) =>
                {
                    if (x.type != ModalType.PickSkill) return;

                    if (x.isActivate)
                    {
                        await _pickSkillView.FadeIn(ModalConfig.TWEEN_DURATION)
                            .WithCancellation(context.CancellationToken);

                        await _pickSkillView.OnClickNextBattle(context.CancellationToken);
                    }
                    else
                    {
                        await _pickSkillView.FadeOut(ModalConfig.TWEEN_DURATION)
                            .WithCancellation(context.CancellationToken);
                    }
                })
                .AddTo(_pickSkillView);
        }
    }
}