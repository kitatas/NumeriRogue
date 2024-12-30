using System.Threading;
using Cysharp.Threading.Tasks;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class OrderView : MonoBehaviour
    {
        [SerializeField] private CardView[] cardViews = default;

        public void Init()
        {
            foreach (var cardView in cardViews)
            {
                cardView.ActivateMask(false);
            }
        }
        
        public async UniTask RenderAsync(int index, CardVO card, CancellationToken token)
        {
            if (cardViews.TryGetValue(index, out var cardView))
            {
                if (card == null)
                {
                }
                else
                {
                    await cardView.RenderAsync(card, token);
                    cardView.Open(CardConfig.ROTATE_SPEED).WithCancellation(token).Forget();
                }
            }
        }
    }
}