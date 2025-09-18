using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common.Utility;
using R3;
using R3.Triggers;
using TMPro;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Image main = default;
        [SerializeField] private Image background = default;
        [SerializeField] private Image mask = default;
        [SerializeField] private TextMeshProUGUI orderNo = default;

        public async UniTask RenderAsync(CardVO card, CancellationToken token)
        {
            Activate(true);
            ActivateMain(false);
            ActivateBackground(true);
            ActivateMask(false);
            RenderOrderNo(0);
            var sprite = await ResourceHelper.LoadAsync<Sprite>(card.imgPath, token);
            main.sprite = sprite;
        }

        public Observable<Unit> Order()
        {
            return main
                .OnPointerDownAsObservable()
                .Select(_ => Unit.Default);
        }

        public void Activate(bool value) => gameObject.SetActive(value);
        public void ActivateMain(bool value) => main.gameObject.SetActive(value);
        public void ActivateBackground(bool value) => background.gameObject.SetActive(value);
        public void ActivateMask(bool value) => mask.gameObject.SetActive(value);
        public void SwitchMask() => ActivateMask(!isOrder);
        public bool isOrder => mask.gameObject.activeSelf;

        public void RenderOrderNo(int no)
        {
            orderNo.text = isOrder ? ZString.Format("{0}", no) : "";
        }

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
                .AppendCallback(() =>
                {
                    ActivateMain(true);
                    ActivateBackground(false);
                })
                .Append(RotateY(270.0f, 0.0f))
                .Append(RotateY(360.0f, duration));
        }

        public Tween Close(float duration)
        {
            return DOTween.Sequence()
                .Append(RotateY(90.0f, duration))
                .AppendCallback(() =>
                {
                    ActivateMain(false);
                    ActivateBackground(true);
                })
                .Append(RotateY(270.0f, 0.0f))
                .Append(RotateY(360.0f, duration));
        }

        public Tween RotateY(float value, float duration)
        {
            return transform.ToRectTransform()
                .DORotate(new Vector3(0.0f, value, 0.0f), duration, RotateMode.FastBeyond360);
        }

        public Tween FadeIn(float duration)
        {
            return DOTween.Sequence()
                .Append(main.DOFade(0.0f, 0.0f))
                .Join(mask.DOFade(0.0f, 0.0f))
                .Append(background.DOFade(0.0f, duration))
                .SetLink(gameObject);
        }

        public Tween FadeOut(float duration)
        {
            return DOTween.Sequence()
                .Append(background.DOFade(1.0f, duration))
                .Append(main.DOFade(1.0f, 0.0f))
                .Join(mask.DOFade(1.0f, 0.0f))
                .SetLink(gameObject);
        }
    }
}