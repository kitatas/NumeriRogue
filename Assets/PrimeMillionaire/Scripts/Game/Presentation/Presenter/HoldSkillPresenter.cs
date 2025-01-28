using System;
using System.Threading;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class HoldSkillPresenter : IStartable, IDisposable
    {
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly HoldSkillView _holdSkillView;
        private readonly CancellationTokenSource _tokenSource;

        public HoldSkillPresenter(HoldSkillUseCase holdSkillUseCase, HoldSkillView holdSkillView)
        {
            _holdSkillUseCase = holdSkillUseCase;
            _holdSkillView = holdSkillView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            foreach (var skillView in _holdSkillView.skills)
            {
                skillView.OnClickAsObservable()
                    .Subscribe(x =>
                    {
                        // NOTE: 売却 or 廃棄
                        _holdSkillUseCase.RemoveAsync(x, _tokenSource.Token).Forget();
                    })
                    .AddTo(skillView);
            }

            Router.Default
                .SubscribeAwait<HoldSkillVO>(async (x, context) =>
                {
                    await _holdSkillView.RenderAsync(x, context.CancellationToken);
                })
                .AddTo(_holdSkillView);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}