using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class HandView : MonoBehaviour
    {
        private List<CardView> _cardViews = new();

        public async UniTask DealHandAsync(CardView cardView, float duration, CancellationToken token)
        {
            cardView.transform.SetParent(transform);
            _cardViews.Add(cardView);

            await (
                cardView.TweenY(0.0f, duration).WithCancellation(token),
                TweenHandsAsync(duration, token)
            );
        }

        private async UniTask TweenHandsAsync(float duration, CancellationToken token)
        {
            var cardCount = _cardViews.Count;
            var pointX = cardCount.IsEven()
                ? -1.0f * HandConfig.HAND_INTERVAL * (cardCount * 0.5f - 0.5f)
                : -1.0f * HandConfig.HAND_INTERVAL * Mathf.Floor(cardCount * 0.5f);

            for (int i = 0; i < cardCount; i++)
            {
                _cardViews[i].TweenX(pointX, duration);
                pointX += HandConfig.HAND_INTERVAL;
            }

            await UniTaskHelper.DelayAsync(duration, token);
        }

        public async UniTask HideAsync(float duration, CancellationToken token)
        {
            if (_cardViews.Count <= 0) return;

            for (int i = 0; i < _cardViews.Count; i++)
            {
                _cardViews[i].TweenX(-1300.0f, duration)
                    .SetDelay(0.05f * i);
            }

            await UniTaskHelper.DelayAsync(duration + 0.5f, token);

            _cardViews.Clear();
            gameObject.DestroyChildren();
        }

        public async UniTask<(int index, int count)> OrderAsync(CancellationToken token)
        {
            var index = await UniTask.WhenAny(_cardViews
                .Select(x => x.OrderAsync(token)));

            _cardViews[index].SwitchMask();
            return (index, _cardViews.Count(x => x.isOrder));
        }

        public async UniTask RenderOrderNoAsync(int index, int no, CancellationToken token)
        {
            _cardViews[index].RenderOrderNo(no);
            await UniTask.Yield(token);
        }
    }
}