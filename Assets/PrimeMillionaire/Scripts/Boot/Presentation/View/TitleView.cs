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

        public void Init()
        {
            touch
                .DOFade(0.0f, UiConfig.FLASH_DURATION)
                .SetEase(Ease.InQuad)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(touch.gameObject);
        }

        public Observable<PointerEventData> push => start.OnPointerDownAsObservable();
    }
}