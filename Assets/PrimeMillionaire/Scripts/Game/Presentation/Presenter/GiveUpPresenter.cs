using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class GiveUpPresenter : IStartable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly ModalUseCase _modalUseCase;
        private readonly GiveUpView _giveUpView;

        public GiveUpPresenter(SceneUseCase sceneUseCase, ModalUseCase modalUseCase, GiveUpView giveUpView)
        {
            _sceneUseCase = sceneUseCase;
            _modalUseCase = modalUseCase;
            _giveUpView = giveUpView;
        }

        public void Start()
        {
            _giveUpView.giveUp
                .SubscribeAwait(async (_, token) =>
                {
                    // delete interrupt
                    await _modalUseCase.ShowAsync(ModalType.GiveUpComplete, token);
                })
                .AddTo(_giveUpView);

            _giveUpView.back
                .Subscribe(_ => _sceneUseCase.Load(SceneName.Top, LoadType.Fade))
                .AddTo(_giveUpView);
        }
    }
}