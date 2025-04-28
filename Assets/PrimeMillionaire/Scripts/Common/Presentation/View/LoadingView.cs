using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loading = default;

        private Sequence _sequence;

        private void Awake()
        {
            _sequence = DOTween.Sequence();
            var animator = new DOTweenTMPAnimator(loading);
            var offset = Vector3.up * 10.0f;
            var length = animator.textInfo.characterCount;

            for (int i = 0; i < length; i++)
            {
                if (char.IsWhiteSpace(loading.text[i])) continue;

                var interval = (i + 1) * 0.05f;
                _sequence
                    .Join(DOTween.Sequence()
                        .AppendInterval(0.05f)
                        .Append(animator
                            .DOOffsetChar(i, animator.GetCharOffset(i) + offset, 0.1f)
                            .SetLoops(2, LoopType.Yoyo)
                            .SetDelay(interval))
                        .AppendInterval(length * 0.05f - interval));
            }

            _sequence
                .SetLoops(-1)
                .SetLink(loading.gameObject);
        }

        public void Activate(bool value)
        {
            loading.enabled = value;

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