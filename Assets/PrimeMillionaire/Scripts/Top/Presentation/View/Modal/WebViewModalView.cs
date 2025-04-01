using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Top.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View.Modal
{
    public sealed class WebViewModalView : TopModalView
    {
        [SerializeField] private Canvas canvas = default;
        [SerializeField] private RectTransform view = default;
        private static WebViewObject _webViewObject = null;

        public override async UniTask ShowAsync(float duration, CancellationToken token)
        {
            SetUpWebView();
            await base.ShowAsync(duration, token);
            RenderWebView();
        }

        public override async UniTask HideAsync(float duration, CancellationToken token)
        {
            DestroyWebView();
            await base.HideAsync(duration, token);
        }

        private void SetUpWebView()
        {
            _webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();
            _webViewObject.Init(enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            _webViewObject.bitmapRefreshCycle = 1;
#endif

            var (left, top, right, bottom) = view.GetMargins(canvas);
            _webViewObject.SetMargins(left, top, right, bottom);
            _webViewObject.LoadURL(type.ToURL());
        }

        private void RenderWebView()
        {
            this.Delay(0.05f, () =>
            {
                // ポップアップ完了後に表示する
                _webViewObject.SetVisibility(true);
            });
        }

        private static void DestroyWebView()
        {
            if (_webViewObject == null) return;

            _webViewObject.SetVisibility(false);
            Destroy(_webViewObject.gameObject);
        }
    }
}