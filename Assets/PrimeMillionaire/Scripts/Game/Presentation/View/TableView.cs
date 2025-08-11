using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class TableView : MonoBehaviour
    {
        [SerializeField] private List<CardView> playerCards = default;
        [SerializeField] private List<CardView> enemyCads = default;

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

        public async UniTask RenderPlayerHandsAsync(List<HandVO> playerHands, CancellationToken token)
        {
            for (int i = 0; i < playerHands.Count; i++)
            {
                var card = playerCards[i];
                card.transform.position = deck.position;
                card.RenderAsync(playerHands[i].card, token).Forget();
                await playerHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
            }
        }

        public async UniTask RenderEnemyHandsAsync(List<HandVO> enemyHands, CancellationToken token)
        {
            await UniTaskHelper.DelayAsync(HandConfig.TWEEN_DURATION / 2.0f, token);
            for (int i = 0; i < enemyHands.Count; i++)
            {
                var card = enemyCads[i];
                card.transform.position = deck.position;
                card.RenderAsync(enemyHands[i].card, token).Forget();
                await enemyHandView.DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
            }
        }

        public async UniTask<(int index, int count)> OrderPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.OrderAsync(token);
        }

        public void OrderEnemyHands(int index)
        {
            enemyHandView.SwitchMask(index);
        }

        public async UniTask RenderPlayerOrderNo(int index, int no, CancellationToken token)
        {
            await playerHandView.RenderOrderNoAsync(index, no, token);
        }

        public async UniTask RenderEnemyOrderNo(int index, int no, CancellationToken token)
        {
            await enemyHandView.RenderOrderNoAsync(index, no, token);
        }

        public async UniTask<IEnumerable<int>> TrashPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.TrashCards(Side.Player, HandConfig.TRASH_DURATION, token);
        }

        public async UniTask<IEnumerable<int>> TrashEnemyHandsAsync(CancellationToken token)
        {
            return await enemyHandView.TrashCards(Side.Enemy, HandConfig.TRASH_DURATION, token);
        }

        public async UniTask ActivatePlayerFieldAsync(float duration, CancellationToken token)
        {
            await playerHandView.ActivateHandsField(true, duration)
                .WithCancellation(token);
        }

        public async UniTask DeactivatePlayerFieldAsync(float duration, CancellationToken token)
        {
            await playerHandView.ActivateHandsField(false, duration)
                .WithCancellation(token);
        }
    }
}