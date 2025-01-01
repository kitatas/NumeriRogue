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
                var card = await CreateCardAsync(playerHand, token);
                await DealPlayerHandAsync(card, token);
            }
        }

        public async UniTask RenderEnemyHandsAsync(IEnumerable<HandVO> enemyHands, CancellationToken token)
        {
            await UniTaskHelper.DelayAsync(HandConfig.TWEEN_DURATION / 2.0f, token);
            foreach (var enemyHand in enemyHands)
            {
                var card = await CreateCardAsync(enemyHand, token);
                await DealEnemyHandAsync(card, token);
            }
        }

        public async UniTask<CardView> CreateCardAsync(HandVO hand, CancellationToken token)
        {
            var card = Instantiate(cardView, transform);
            card.transform.localPosition = deck.localPosition;
            await card.RenderAsync(hand.card, token);
            return card;
        }

        public async UniTask DealPlayerHandAsync(CardView card, CancellationToken token)
        {
            await playerHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
            card.Open(CardConfig.ROTATE_SPEED).WithCancellation(token).Forget();
        }

        public async UniTask DealEnemyHandAsync(CardView card, CancellationToken token)
        {
            await enemyHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
        }

        public async UniTask<(int index, int count)> OrderPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.OrderAsync(token);
        }

        public async UniTask RenderPlayerOrderNo(int index, int no, CancellationToken token)
        {
            await playerHandView.RenderOrderNoAsync(index, no, token);
        }

        public async UniTask<IEnumerable<int>> TrashPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.TrashCards(Side.Player, HandConfig.TRASH_DURATION, token);
        }

        public async UniTask TrashEnemyHandAsync(int index, CancellationToken token)
        {
            await enemyHandView.HideAsync(Side.Enemy, index, HandConfig.TRASH_DURATION, token);
        }

        public void DestroyHideCards()
        {
            playerHandView.DestroyCards();
            enemyHandView.DestroyCards();
        }
    }
}