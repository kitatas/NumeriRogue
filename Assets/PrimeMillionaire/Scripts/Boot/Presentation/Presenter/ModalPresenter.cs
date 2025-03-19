using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Presentation.View.Modal;
using PrimeMillionaire.Common;
using UnityEngine;
using VContainer.Unity;
using VitalRouter;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class ModalPresenter : IAsyncStartable
    {
        public async UniTask StartAsync(CancellationToken token)
        {
            var modalViews = Object.FindObjectsByType<BootModalView>(FindObjectsSortMode.None).ToList();
            await UniTask.WhenAll(modalViews
                .Select(x => x.InitAsync(token)));

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