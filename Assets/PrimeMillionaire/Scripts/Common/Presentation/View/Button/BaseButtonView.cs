using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimeMillionaire.Common.Presentation.View.Button
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button button = default;

        public Observable<PointerEventData> push => button.OnPointerClickAsObservable();
        public UniTask OnClickAsync(CancellationToken token) => button.OnClickAsync(token);

        private void Awake()
        {
            button.OnPointerDownAsObservable()
                .Subscribe(_ => TweenScale(0.95f, UiConfig.BUTTON_DURATION))
                .AddTo(this);

            button.OnPointerUpAsObservable()
                .Subscribe(_ => TweenScale(1.0f, UiConfig.BUTTON_DURATION))
                .AddTo(this);
        }

        private Tween TweenScale(float target, float duration)
        {
            return button.transform
                .DOScale(target, duration)
                .SetLink(button.gameObject);
        }

        public void SetInteractable(bool value) => button.interactable = value;
    }
}