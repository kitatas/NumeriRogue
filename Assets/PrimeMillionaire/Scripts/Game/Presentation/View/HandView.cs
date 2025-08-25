using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Utility;
using R3;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class HandView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private List<CardView> cards = default;
        private readonly List<CardView> _cardViews = new();

        public async UniTask DealAsync(List<HandVO> hands, Transform deck, float duration, CancellationToken token)
        {
            _cardViews.Clear();

            for (int i = 0; i < hands.Count; i++)
            {
                var cardView = cards[i];
                cardView.transform.position = deck.position;
                cardView.RenderAsync(hands[i].card, token).Forget();

                _cardViews.Add(cardView);

                await (
                    cardView.TweenY(0.0f, duration).WithCancellation(token),
                    TweenHandsAsync(duration, token)
                );

                cardView.Open(duration).WithCancellation(token).Forget();
            }
        }

        public async UniTask RepaintAsync(List<HandVO> hands, CancellationToken token)
        {
            for (int i = 0; i < hands.Count; i++)
            {
                var cardView = _cardViews[i];
                cardView.RenderAsync(hands[i].card, token).Forget();
                cardView.Open(0.0f).WithCancellation(token).Forget();
            }

            await UniTask.Yield(token);
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

        public List<Observable<int>> OrderAll()
        {
            return _cardViews
                .Select(x => x.Order().Select(_ => _cardViews.IndexOf(x)))
                .ToList();
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

        public Tween ActivateHandsField(float duration)
        {
            return DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(1.0f, duration))
                .AppendCallback(() => canvasGroup.blocksRaycasts = true)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }

        public Tween DeactivateHandsField(float duration)
        {
            return DOTween.Sequence()
                .AppendCallback(() => canvasGroup.blocksRaycasts = false)
                .Append(canvasGroup
                    .DOFade(0.5f, duration))
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }
    }
}