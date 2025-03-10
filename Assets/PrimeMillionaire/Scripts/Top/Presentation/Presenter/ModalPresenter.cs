using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot;
using PrimeMillionaire.Common;
using PrimeMillionaire.Top.Presentation.View.Modal;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class ModalPresenter : IAsyncStartable
    {
        private readonly List<BaseModalView> _modalViews;

        public ModalPresenter(IEnumerable<BaseModalView> modalViews)
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

                    await modalView.PopupAsync(UiConfig.POPUP_DURATION, context.CancellationToken);
                })
                .AddTo(token);
        }
    }
}