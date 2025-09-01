using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common.Utility;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimeMillionaire.Common.Presentation.View.Button
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private ButtonType type = default;
        [SerializeField] private UnityEngine.UI.Button button = default;

        private bool _isSetUp;

        public Observable<PointerEventData> push => button.OnPointerClickAsObservable().Where(_ => button.interactable);
        public UniTask OnClickAsync(CancellationToken token) => button.OnClickAsync(token);

        private void Awake()
        {
            _isSetUp = false;

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

        public void SetUp(Action<Se, float> playSe)
        {
            if (_isSetUp) return;
            _isSetUp = true;

            push.Subscribe(_ => playSe?.Invoke(type.ToSe(), 0.0f))
                .AddTo(this);
        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
            button.image.raycastTarget = value;
        }
    }
}