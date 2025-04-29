using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.View.Button;
using PrimeMillionaire.Game.Presentation.View.Modal;
using R3;
using UnityEngine;
using VContainer.Unity;
using VitalRouter;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class ModalPresenter : IAsyncStartable
    {
        private readonly ModalUseCase _modalUseCase;

        public ModalPresenter(ModalUseCase modalUseCase)
        {
            _modalUseCase = modalUseCase;
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            var modalViews = Object.FindObjectsByType<GameModalView>(FindObjectsSortMode.None).ToList();
            await UniTask.WhenAll(modalViews
                .Select(x => x.InitAsync(token)));

            var modalButtons = Object.FindObjectsByType<GameModalButtonView>(FindObjectsSortMode.None).ToList();
            foreach (var button in modalButtons)
            {
                button.push
                    .Subscribe(_ => _modalUseCase.PopupAsync(button.type, button.isActivate, token).Forget())
                    .AddTo(button);
            }

            Router.Default
                .SubscribeAwait<ModalVO>(async (v, context) =>
                {
                    var modalView = modalViews.Find(x => x.type == v.type);
                    if (modalView == null)
                    {
                        throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_MODAL);
                    }

                    if (v.isActivate)
                    {
                        await modalView.ShowAsync(UiConfig.POPUP_DURATION, context.CancellationToken);
                    }
                    else
                    {
                        await modalView.HideAsync(UiConfig.POPUP_DURATION, context.CancellationToken);
                    }
                })
                .AddTo(token);
        }
    }
}