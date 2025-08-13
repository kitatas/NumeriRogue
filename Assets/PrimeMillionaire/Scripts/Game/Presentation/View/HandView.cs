using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class HandView : MonoBehaviour
    {
        private readonly List<CardView> _cardViews = new();

        public async UniTask DealHandAsync(CardView cardView, float duration, CancellationToken token)
        {
            _cardViews.Add(cardView);

            await (
                cardView.TweenY(0.0f, duration).WithCancellation(token),
                TweenHandsAsync(duration, token)
            );

            cardView.Open(UiConfig.TWEEN_DURATION).WithCancellation(token).Forget();
        }

        private async UniTask TweenHandsAsync(float duration, CancellationToken token)
        {
            var cardCount = _cardViews.Count;
            var pointX = cardCount.IsEven()
                ? -1.0f * HandConfig.HAND_INTERVAL * (cardCount * 0.5f - 0.5f)
                : -1.0f * HandConfig.HAND_INTERVAL * Mathf.Floor(cardCount * 0.5f);

            for (int i = 0; i < cardCount; i++)
            {
                _cardViews[i].TweenX(pointX, duration).WithCancellation(token).Forget();
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
                    .SetDelay(0.05f * i)
                    .WithCancellation(token)
                    .Forget();
            }

            await UniTaskHelper.DelayAsync(duration + 0.5f, token);

            _cardViews.Clear();
        }

        public async UniTask<(int index, int count)> OrderAsync(CancellationToken token)
        {
            var index = await UniTask.WhenAny(_cardViews
                .Select(x => x.OrderAsync(token)));

            _cardViews[index].SwitchMask();
            return (index, _cardViews.Count(x => x.isOrder));
        }

        public async UniTask OrderAsync(int index, CancellationToken token)
        {
            _cardViews[index].SwitchMask();
            await UniTaskHelper.DelayAsync(HandConfig.TRASH_DURATION, token);
        }

        public async UniTask RenderOrderNoAsync(int index, int no, CancellationToken token)
        {
            _cardViews[index].RenderOrderNo(no);
            await UniTask.Yield(token);
        }

        public async UniTask HideAsync(Side side, int index, float duration, CancellationToken token)
        {
            await _cardViews[index]
                .TweenY(300.0f * side.ToSign(), duration)
                .WithCancellation(token);

            _cardViews.RemoveAt(index);
            await TweenHandsAsync(duration, token);
        }

        public async UniTask<IEnumerable<int>> TrashCards(Side side, float duration, CancellationToken token)
        {
            var index = new List<int>();
            for (int i = _cardViews.Count - 1; i >= 0; i--)
            {
                if (_cardViews[i].isOrder)
                {
                    index.Add(i);
                    await HideAsync(side, i, duration, token);
                }
            }

            return index;
        }

        public Tween ActivateHandsField(bool value, float duration)
        {
            var y = value ? 200.0f : 0.0f;
            return DOTween.Sequence()
                .Append(transform.ToRectTransform()
                    .DOAnchorPosY(y, duration))
                .SetEase(Ease.OutCirc);
        }
    }
}