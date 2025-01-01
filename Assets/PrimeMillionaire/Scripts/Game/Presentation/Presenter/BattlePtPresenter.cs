using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class BattlePtPresenter : IStartable
    {
        private readonly BattlePtUseCase _battlePtUseCase;
        private readonly BattlePtView _battlePtView;

        public BattlePtPresenter(BattlePtUseCase battlePtUseCase, BattlePtView battlePtView)
        {
            _battlePtUseCase = battlePtUseCase;
            _battlePtView = battlePtView;
        }

        public void Start()
        {
            _battlePtUseCase.playerPt
                .Subscribe(_battlePtView.RenderPlayer)
                .AddTo(_battlePtView);

            _battlePtUseCase.enemyPt
                .Subscribe(_battlePtView.RenderEnemy)
                .AddTo(_battlePtView);
        }
    }
}