using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using R3;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly TransitionView _transitionView;

        public ScenePresenter(SceneUseCase sceneUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
            _transitionView = transitionView;
        }

        public void Initialize()
        {
            _sceneUseCase.load
                .SubscribeAwait(async (x, token) =>
                {
                    // TODO: transition
                    await SceneManager.LoadSceneAsync(x.name).ToUniTask(cancellationToken: token);
                })
                .AddTo(_transitionView);
        }
    }
}