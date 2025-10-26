using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private TextMeshProUGUI loading = default;

        private readonly float _interval = 0.05f;
        private Sequence _sequence;
        private Tween _fade;

        private void Awake()
        {
            _sequence = DOTween.Sequence();
            var animator = new DOTweenTMPAnimator(loading);
            var offset = Vector3.up * 10.0f;
            var length = animator.textInfo.characterCount;

            for (int i = 0; i < length; i++)
            {
                if (char.IsWhiteSpace(loading.text[i])) continue;

                var interval = (i + 1) * _interval;
                _sequence
                    .Join(DOTween.Sequence()
                        .AppendInterval(_interval)
                        .Append(animator
                            .DOOffsetChar(i, animator.GetCharOffset(i) + offset, 0.1f)
                            .SetLoops(2, LoopType.Yoyo)
                            .SetDelay(interval))
                        .AppendInterval(length * _interval - interval));
            }

            _sequence
                .SetLoops(-1)
                .SetLink(loading.gameObject);
        }

        public void Activate(bool value)
        {
            _fade?.Kill();
            _fade = canvasGroup
                .DOFade(value ? 1.0f : 0.0f, 0.05f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);

            if (value)
            {
                _sequence?.Restart();
            }
            else
            {
                _sequence?.Pause();
            }
        }
    }
}