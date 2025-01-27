using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class HoldSkillPresenter : IStartable
    {
        private readonly HoldSkillView _holdSkillView;

        public HoldSkillPresenter(HoldSkillView holdSkillView)
        {
            _holdSkillView = holdSkillView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<HoldSkillVO>(async (x, context) =>
                {
                    await _holdSkillView.RenderAsync(x, context.CancellationToken);
                })
                .AddTo(_holdSkillView);
        }
    }
}