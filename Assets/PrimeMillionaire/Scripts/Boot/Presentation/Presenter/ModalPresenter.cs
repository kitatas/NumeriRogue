using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Presentation.View.Modal;
using PrimeMillionaire.Common;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class ModalPresenter : IAsyncStartable
    {
        private readonly List<BootModalView> _modalViews;

        public ModalPresenter(IEnumerable<BootModalView> modalViews)
        {
            _modalViews = modalViews.ToList();
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            await UniTask.WhenAll(_modalViews
                .Select(x => x.InitAsync(token)));

            Router.Default
                .SubscribeAwait<ModalVO>(async (v, context) =>
                {
                    var modalView = _modalViews.Find(x => x.type == v.type);
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