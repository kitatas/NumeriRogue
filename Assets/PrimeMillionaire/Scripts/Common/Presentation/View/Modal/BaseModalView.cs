using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View.Modal
{
    public abstract class BaseModalView : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup = default;
        private Tween _tween;

        public async UniTask InitAsync(CancellationToken token)
        {
            await HideAsync(0.0f, token);
        }

        public virtual async UniTask ShowAsync(float duration, CancellationToken token)
        {
            await Show(duration).WithCancellation(token);
        }

        public virtual async UniTask HideAsync(float duration, CancellationToken token)
        {
            await Hide(duration).WithCancellation(token);
        }

        public virtual Tween Show(float duration)
        {
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .AppendCallback(() => canvasGroup.blocksRaycasts = true)
                .Append(canvasGroup
                    .DOFade(1.0f, duration)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBack))
                .SetLink(gameObject);

            return _tween;
        }

        public virtual Tween Hide(float duration)
        {
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(0.0f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one * 0.8f, duration)
                    .SetEase(Ease.OutQuart))
                .AppendCallback(() => canvasGroup.blocksRaycasts = false)
                .SetLink(gameObject);

            return _tween;
        }
    }
}