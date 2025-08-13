using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
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

        public async UniTask RenderHandsAsync(Side side, List<HandVO> hands, CancellationToken token)
        {
            if (side == Side.Enemy)
            {
                await UniTaskHelper.DelayAsync(HandConfig.TWEEN_DURATION / 2.0f, token);
            }

            for (int i = 0; i < hands.Count; i++)
            {
                var card = GetCardViews(side)[i];
                card.transform.position = deck.position;
                card.RenderAsync(hands[i].card, token).Forget();
                await GetHandView(side).DealHandAsync(card, HandConfig.TWEEN_DURATION, token);
            }
        }

        public async UniTask<(int index, int count)> OrderPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.OrderAsync(token);
        }

        public async UniTask OrderEnemyHandsAsync(int index, CancellationToken token)
        {
            await enemyHandView.OrderAsync(index, token);
        }

        public async UniTask RenderOrderNo(Side side, int index, int no, CancellationToken token)
        {
            await GetHandView(side).RenderOrderNoAsync(index, no, token);
        }

        public async UniTask<IEnumerable<int>> TrashHandsAsync(Side side, CancellationToken token)
        {
            return await GetHandView(side).TrashCards(side, HandConfig.TRASH_DURATION, token);
        }

        private List<CardView> GetCardViews(Side side)
        {
            return side switch
            {
                Side.Player => playerCards,
                Side.Enemy => enemyCads,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        private HandView GetHandView(Side side)
        {
            return side switch
            {
                Side.Player => playerHandView,
                Side.Enemy => enemyHandView,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
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