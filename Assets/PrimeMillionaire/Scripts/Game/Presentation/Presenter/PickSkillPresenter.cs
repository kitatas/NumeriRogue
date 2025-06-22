using System;
using System.Threading;
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
                        skillView.ActivateSoldOut(true);
                        _dollarUseCase.Consume(x.price);
                        _holdSkillUseCase.AddAsync(x, _tokenSource.Token).Forget();
                    })
                    .AddTo(skillView);
            }

            _dollarUseCase.dollar
                .Subscribe(_pickSkillView.Repaint)
                .AddTo(_pickSkillView);

            _holdSkillUseCase.isFull
                .Select(x => x ? 0 : _dollarUseCase.currentValue)
                .Subscribe(_pickSkillView.Repaint)
                .AddTo(_pickSkillView);

            Router.Default
                .SubscribeAwait<PickSkillVO>(async (x, context) =>
                {
                    var isConsume = _dollarUseCase.IsConsume(x.skill.price) && _holdSkillUseCase.hasEmpty;
                    await _pickSkillView.RenderAsync(x, isConsume, context.CancellationToken);
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