using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View.Modal
{
    public abstract class BaseModalView: MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup = default;
        [SerializeField] private Button button = default;
        private Tween _tween;

        public abstract ModalType type { get; }

        public virtual async UniTask InitAsync(CancellationToken token)
        {
            await Hide(0.0f).WithCancellation(token);
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

        public virtual async UniTask PopupAsync(float duration, CancellationToken token)
        {
            await Show(duration).WithCancellation(cancellationToken: token);
            await button.OnClickAsync(token);
            await Hide(duration).WithCancellation(cancellationToken: token);
        }
    }
}