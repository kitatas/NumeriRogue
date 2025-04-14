using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common;
using TMPro;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class OrderView : MonoBehaviour
    {
        [SerializeField] private CardView[] cardViews = default;
        [SerializeField] private TextMeshProUGUI currentValue = default;
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
                    cardView.Close(UiConfig.TWEEN_DURATION).WithCancellation(token).Forget();
                }
                else
                {
                    await cardView.RenderAsync(card, token);
                    cardView.Open(UiConfig.TWEEN_DURATION).WithCancellation(token).Forget();
                }
            }
        }

        public async UniTask SetAsync(OrderValueVO orderValue, CancellationToken token)
        {
            var value = orderValue.value;
            await TweenOrderValue(value).WithCancellation(token);

            foreach (var bonus in orderValue.bonus)
            {
                value = (int)(value * bonus.value);
                await bonusView.TweenAsync(bonus, token);
                await TweenOrderValue(value).WithCancellation(token);
            }
        }

        private Tween TweenOrderValue(int value)
        {
            return DOTween.Sequence()
                .Append(currentValue
                    .DOFade(1.0f, UiConfig.TWEEN_DURATION))
                .Append(DOTween.To(
                    () => int.Parse(currentValue.text),
                    x => currentValue.text = ZString.Format("{0}", x),
                    value,
                    UiConfig.TWEEN_DURATION));
        }

        public async UniTask FadeInCardsAsync(float duration, CancellationToken token)
        {
            await UniTask.WhenAll(cardViews
                .Select(x => x.FadeIn(duration).WithCancellation(token)));
        }

        public async UniTask FadeOutCardsAsync(float duration, CancellationToken token)
        {
            await UniTask.WhenAll(cardViews
                .Select(x => x.FadeOut(duration).WithCancellation(token)));
        }
    }
}