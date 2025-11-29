using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View;
using PrimeMillionaire.Common.Utility;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly TransitionView _transitionView;

        public ScenePresenter(LoadingUseCase loadingUseCase, SceneUseCase sceneUseCase, TransitionView transitionView)
        {
            _loadingUseCase = loadingUseCase;
            _sceneUseCase = sceneUseCase;
            _transitionView = transitionView;
        }

        public void Initialize()
        {
            _sceneUseCase.load
                .SubscribeAwait(async (x, token) =>
                {
                    if (x.isFade) await _transitionView.FadeIn(0.1f).WithCancellation(token);

                    // シーン内の全リソース解放してから遷移させる
                    ResourceHelper.Release();
                    await ResourceHelper.LoadSceneAsync(x.path, token);

                    _loadingUseCase.Set(true);
                    await UniTaskHelper.DelayAsync(0.5f, token);

                    await UniTask.WaitWhile(() => _loadingUseCase.isLoading, cancellationToken: token);
                    await UniTaskHelper.DelayAsync(0.5f, token);

                    if (x.isFade) await _transitionView.FadeOut(0.1f).WithCancellation(token);
                })
                .AddTo(_transitionView);

            _transitionView.FadeOut(0.0f);
        }
    }
}