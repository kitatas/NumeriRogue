using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Game.Utility;
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
                    cardView.Close(CardConfig.ROTATE_SPEED).WithCancellation(token).Forget();
                }
                else
                {
                    await cardView.RenderAsync(card, token);
                    cardView.Open(CardConfig.ROTATE_SPEED).WithCancellation(token).Forget();
                }
            }
        }

        public async UniTask SetAsync(OrderValueVO orderValue, CancellationToken token)
        {
            var value = orderValue.value;
            await TweenOrderValue(value).WithCancellation(token);

            foreach (var type in orderValue.bonus.types)
            {
                value = (int)(value * type.ToBonus());
                await bonusView.TweenAsync(type, token);
                await TweenOrderValue(value).WithCancellation(token);
            }
        }

        private Tween TweenOrderValue(int value)
        {
            return DOTween.Sequence()
                .Append(currentValue
                    .DOFade(1.0f, OrderConfig.TWEEN_DURATION))
                .Append(DOTween.To(
                    () => int.Parse(currentValue.text),
                    x => currentValue.text = $"{x}",
                    value,
                    OrderConfig.TWEEN_DURATION));
        }

        public async UniTask FadeCardsAsync(float value, float duration, CancellationToken token)
        {
            await UniTask.WhenAll(cardViews
                .Select(x => x.Fade(value, duration).WithCancellation(token)));
        }
    }
}