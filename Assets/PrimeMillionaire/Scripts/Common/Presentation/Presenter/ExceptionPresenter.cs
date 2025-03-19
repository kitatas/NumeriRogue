using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View.Modal;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Common.Presentation.Presenter
{
    public sealed class ExceptionPresenter : IInitializable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly ExceptionModalView _exceptionModalView;

        public ExceptionPresenter(SceneUseCase sceneUseCase, ExceptionModalView exceptionModalView)
        {
            _sceneUseCase = sceneUseCase;
            _exceptionModalView = exceptionModalView;
        }

        public void Initialize()
        {
            _exceptionModalView.Hide(0.0f);

            Router.Default
                .SubscribeAwait<ExceptionVO>(async (v, context) =>
                {
                    _exceptionModalView.Render(v.Message);
                    await _exceptionModalView.ShowAndClickAsync(UiConfig.POPUP_DURATION, context.CancellationToken);
                    switch (v)
                    {
                        case RebootExceptionVO:
                            _exceptionModalView.Hide(UiConfig.POPUP_DURATION);
                            _sceneUseCase.Load(SceneName.Boot);
                            break;
                        case RetryExceptionVO:
                            _exceptionModalView.Hide(UiConfig.POPUP_DURATION);
                            break;
                        default:
                            // QuitException含む意図しないExceptionの場合は強制終了
#if UNITY_ANDROID
                            UnityEngine.Application.Quit();
#else
                            // TODO: android以外の場合
#endif
                            break;
                    }
                })
                .AddTo(_exceptionModalView);
        }
    }
}