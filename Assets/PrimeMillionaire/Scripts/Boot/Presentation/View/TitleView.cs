using DG.Tweening;
using PrimeMillionaire.Common;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI touch = default;
        [SerializeField] private Button start = default;

        private Tween _tween;

        public void Init()
        {
            _tween = touch
                .DOFade(0.0f, UiConfig.FLASH_DURATION)
                .SetEase(Ease.InQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(touch.gameObject);

            Activate(false);
        }

        public void Activate(bool value)
        {
            if (value)
            {
                touch.text = $"Touch Screen";
                _tween?.Restart();
            }
            else
            {
                touch.text = $"";
                _tween?.Pause();
            }
        }

        public Observable<PointerEventData> push => start.OnPointerDownAsObservable();
    }
}