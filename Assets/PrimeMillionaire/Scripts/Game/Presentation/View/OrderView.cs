using System;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View;
using TMPro;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class OrderView : MonoBehaviour
    {
        [SerializeField] private CardView[] cardViews = default;
        [SerializeField] private TextMeshProUGUI currentValue = default;
        [SerializeField] private Image currentValueBg = default;
        [SerializeField] private BonusView bonusView = default;

        public void Init()
        {
            foreach (var cardView in cardViews)
            {
                cardView.ActivateMask(false);
            }

            currentValue.text = "0";
            bonusView.Hide(0.0f);
        }

        public async UniTask RenderAsync(int index, CardVO card, CancellationToken token)
        {
            if (cardViews.TryGetValue(index, out var cardView))
            {
                if (card == null)
                {
                    cardView.Close(CardConfig.FLIP_DURATION).WithCancellation(token).Forget();
                }
                else
                {
                    await cardView.RenderAsync(card, token);
                    cardView.Open(CardConfig.FLIP_DURATION).WithCancellation(token).Forget();
                }
            }
        }

        public async UniTask SetAsync(OrderValueVO orderValue, Action<Se> playSe, CancellationToken token)
        {
            playSe?.Invoke(Se.BattlePt);
            var value = orderValue.value;
            await TweenOrderValue(value).WithCancellation(token);

            foreach (var bonus in orderValue.bonus)
            {
                value = Mathf.CeilToInt(value * bonus.value);
                await bonusView.TweenAsync(bonus, token);
                playSe?.Invoke(Se.BattlePt);
                await TweenOrderValue(value).WithCancellation(token);
            }
        }

        private Tween TweenOrderValue(int value)
        {
            return DOTween.To(
                () => int.Parse(currentValue.text),
                x => currentValue.text = ZString.Format("{0}", x),
                value,
                UiConfig.TWEEN_DURATION);
        }

        public async UniTask FadeInCardsAsync(float duration, CancellationToken token)
        {
            await (
                UniTask.WhenAll(cardViews
                    .Select(x => x.FadeIn(duration).WithCancellation(token))),
                FadeCurrentValue(0.0f, duration).WithCancellation(token)
            );
        }

        public async UniTask FadeOutCardsAsync(float duration, CancellationToken token)
        {
            await (
                UniTask.WhenAll(cardViews
                    .Select(x => x.FadeOut(duration).WithCancellation(token))),
                FadeCurrentValue(1.0f, duration).WithCancellation(token)
            );
        }

        private Tween FadeCurrentValue(float target, float duration)
        {
            return DOTween.Sequence()
                .Append(currentValue
                    .DOFade(target, duration))
                .Join(currentValueBg
                    .DOFade(target, duration))
                .SetLink(gameObject);
        }
    }
}