using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class OrderView : MonoBehaviour
    {
        [SerializeField] private CardView[] cardViews = default;
        [SerializeField] private TextMeshProUGUI currentValue = default;

        public void Init()
        {
            foreach (var cardView in cardViews)
            {
                cardView.ActivateMask(false);
            }

            currentValue.text = "0";
            currentValue
                .DOFade(0.0f, 0.0f);
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

        public async UniTask SetAsync(int value, CancellationToken token)
        {
            if (value == 0)
            {
                await DOTween.Sequence()
                    .Append(currentValue
                        .DOFade(0.0f, OrderConfig.TWEEN_DURATION))
                    .AppendCallback(() => currentValue.text = "0")
                    .WithCancellation(token);
            }
            else
            {
                await DOTween.Sequence()
                    .Append(currentValue
                        .DOFade(1.0f, OrderConfig.TWEEN_DURATION))
                    .Append(DOTween.To(
                        () => int.Parse(currentValue.text),
                        x => currentValue.text = $"{x}",
                        value,
                        OrderConfig.TWEEN_DURATION))
                    .WithCancellation(token);
            }
        }
    }
}