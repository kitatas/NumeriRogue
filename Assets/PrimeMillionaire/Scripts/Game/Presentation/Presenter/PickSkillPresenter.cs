using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class PickSkillPresenter : IStartable, IDisposable
    {
        private readonly DollarUseCase _dollarUseCase;
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly PickSkillView _pickSkillView;
        private readonly CancellationTokenSource _tokenSource;

        public PickSkillPresenter(DollarUseCase dollarUseCase, HoldSkillUseCase holdSkillUseCase,
            PickSkillView pickSkillView)
        {
            _dollarUseCase = dollarUseCase;
            _holdSkillUseCase = holdSkillUseCase;
            _pickSkillView = pickSkillView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            foreach (var skillView in _pickSkillView.skills)
            {
                skillView.OnClickAsObservable()
                    .Where(x => _dollarUseCase.IsConsume(x.price))
                    .Subscribe(x =>
                    {
                        skillView.SoldOut();
                        _dollarUseCase.Consume(x.price);
                        _holdSkillUseCase.AddAsync(x, _tokenSource.Token).Forget();
                    })
                    .AddTo(skillView);
            }

            _dollarUseCase.dollar
                .Subscribe(_pickSkillView.Repaint)
                .AddTo(_pickSkillView);

            Router.Default
                .SubscribeAwait<PickSkillVO>(async (x, context) =>
                {
                    await _pickSkillView.RenderAsync(x, _dollarUseCase.IsConsume(x.skill.price), context.CancellationToken);
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

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}