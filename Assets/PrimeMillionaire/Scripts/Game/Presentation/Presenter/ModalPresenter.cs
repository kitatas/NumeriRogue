using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Presentation.View.Modal;
using UnityEngine;
using VContainer.Unity;
using VitalRouter;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class ModalPresenter : IAsyncStartable
    {
        public async UniTask StartAsync(CancellationToken token)
        {
            var modalViews = Object.FindObjectsByType<GameModalView>(FindObjectsSortMode.None).ToList();
            await UniTask.WhenAll(modalViews
                .Select(x => x.InitAsync(token)));

            Router.Default
                .SubscribeAwait<ModalVO>(async (v, context) =>
                {
                    var modalView = modalViews.Find(x => x.type == v.type);
                    if (modalView == null) throw new Exception();

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