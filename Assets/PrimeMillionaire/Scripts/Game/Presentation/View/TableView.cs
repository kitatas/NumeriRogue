using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using PrimeMillionaire.Common.Utility;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class TableView : MonoBehaviour
    {
        [SerializeField] private HandView playerHandView = default;
        [SerializeField] private HandView enemyHandView = default;
        [SerializeField] private CommonButtonView sortButtonView = default;
        [SerializeField] private Transform deck = default;

        public async UniTask RenderHandsAsync(Side side, List<HandVO> hands, CancellationToken token)
        {
            if (side == Side.Enemy)
            {
                await UniTaskHelper.DelayAsync(HandConfig.TWEEN_DURATION / 2.0f, token);
            }

            await GetHandView(side).DealAsync(hands, deck, HandConfig.TWEEN_DURATION, token);
        }

        public async UniTask RepaintHandsAsync(Side side, List<HandVO> hands, CancellationToken token)
        {
            await GetHandView(side).RepaintAsync(hands, token);
        }

        public async UniTask<(int index, int count)> OrderPlayerHandsAsync(CancellationToken token)
        {
            return await playerHandView.OrderAsync(token);
        }

        public List<Observable<int>> OrderAll(Side side)
        {
            return GetHandView(side).OrderAll();
        }

        public async UniTask OrderHandsAsync(Side side, int index, CancellationToken token)
        {
            await GetHandView(side).OrderAsync(index, token);
        }

        public async UniTask RenderOrderNo(Side side, int index, int no, CancellationToken token)
        {
            await GetHandView(side).RenderOrderNoAsync(index, no, token);
        }

        public async UniTask TrashHandsAsync(Side side, int index, float duration, CancellationToken token)
        {
            await GetHandView(side).HideAsync(side, index, duration, token);
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

        public Observable<Unit> pushSwitchSort => sortButtonView.push.Select(_ => Unit.Default);

        public async UniTask ActivatePlayerFieldAsync(float duration, CancellationToken token)
        {
            await playerHandView.ActivateHandsField(true, duration)
                .WithCancellation(token);

            sortButtonView.SetInteractable(true);
        }

        public async UniTask DeactivatePlayerFieldAsync(float duration, CancellationToken token)
        {
            sortButtonView.SetInteractable(false);

            await playerHandView.ActivateHandsField(false, duration)
                .WithCancellation(token);
        }
    }
}