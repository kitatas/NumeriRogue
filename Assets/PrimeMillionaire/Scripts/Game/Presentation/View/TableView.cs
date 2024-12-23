using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class TableView : MonoBehaviour
    {
        [SerializeField] private CardView cardView = default;

        [SerializeField] private HandView playerHandView = default;
        [SerializeField] private HandView enemyHandView = default;
        [SerializeField] private Transform deck = default;

        public async UniTask SetUpAsync(CancellationToken token)
        {
            await (
                playerHandView.HideAsync(HandConfig.TWEEN_DURATION, token),
                enemyHandView.HideAsync(HandConfig.TWEEN_DURATION, token)
            );
        }

        public async UniTask RenderPlayerHandsAsync(IEnumerable<HandVO> playerHands, CancellationToken token)
        {
            foreach (var playerHand in playerHands)
            {
                await CreatePlayerHandAsync(playerHand, token);
            }
        }

        public async UniTask RenderEnemyHandsAsync(IEnumerable<HandVO> enemyHands, CancellationToken token)
        {
            await UniTaskHelper.DelayAsync(HandConfig.TWEEN_DURATION / 2.0f, token);
            foreach (var enemyHand in enemyHands)
            {
                await CreateEnemyHandAsync(enemyHand, token);
            }
        }

        public async UniTask CreatePlayerHandAsync(HandVO hand, CancellationToken token)
        {
            var card = Instantiate(cardView, transform);
            card.transform.localPosition = deck.localPosition;
            card.Render(hand.card);
            await playerHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
        }

        public async UniTask CreateEnemyHandAsync(HandVO hand, CancellationToken token)
        {
            var card = Instantiate(cardView, transform);
            card.transform.localPosition = deck.localPosition;
            card.Render(hand.card);
            await enemyHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
        }
    }
}