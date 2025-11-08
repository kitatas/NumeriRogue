using DG.Tweening;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class SplashView : MonoBehaviour
    {
        [SerializeField] private Image copyright = default;
        [SerializeField] private Button screen = default;

        private Tween _tween;

        public Tween Render(SplashVO value)
        {
            Kill();

            copyright.sprite = value.sprite;
            _tween = DOTween.Sequence()
                .Append(copyright.DOFade(1.0f, value.duration))
                .AppendInterval(1.0f)
                .Append(copyright.DOFade(0.0f, value.duration))
                .SetEase(Ease.Linear)
                .SetLink(gameObject);

            return _tween;
        }

        public void Kill()
        {
            _tween?.Kill(true);
        }

        public Observable<PointerEventData> push => screen.OnPointerDownAsObservable();
    }
}