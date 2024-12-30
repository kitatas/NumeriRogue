using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using PrimeMillionaire.Common.Utility;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Image main = default;
        [SerializeField] private Image background = default;
        [SerializeField] private Image mask = default;

        public async UniTask RenderAsync(CardVO card, CancellationToken token)
        {
            ActivateMask(false);
            var sprite = await ResourceHelper.LoadAsync<Sprite>(card.imgPath, token);
            main.sprite = sprite;
        }

        public UniTask OrderAsync(CancellationToken token)
        {
            return main.GetAsyncPointerDownTrigger().OnPointerDownAsync(token).AsUniTask();
        }

        public void ActivateBackground(bool value) => background.gameObject.SetActive(value);
        public void ActivateMask(bool value) => mask.gameObject.SetActive(value);
        public void SwitchMask() => ActivateMask(!isOrder);
        public bool isOrder => mask.gameObject.activeSelf;

        public Tween TweenX(float value, float duration)
        {
            return transform.ToRectTransform()
                .DOAnchorPosX(value, duration);
        }

        public Tween TweenY(float value, float duration)
        {
            return transform.ToRectTransform()
                .DOAnchorPosY(value, duration);
        }

        public Tween Open(float duration)
        {
            return DOTween.Sequence()
                .Append(RotateY(90.0f, duration))
                .AppendCallback(() => ActivateBackground(false))
                .Append(RotateY(270.0f, 0.0f))
                .Append(RotateY(360.0f, duration));
        }

        public Tween Close(float duration)
        {
            return DOTween.Sequence()
                .Append(RotateY(90.0f, duration))
                .AppendCallback(() => ActivateBackground(true))
                .Append(RotateY(270.0f, 0.0f))
                .Append(RotateY(360.0f, duration));
        }

        public Tween RotateY(float value, float duration)
        {
            return transform.ToRectTransform()
                .DORotate(new Vector3(0.0f, value, 0.0f), duration, RotateMode.FastBeyond360);
        }
    }
}