using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View.Modal
{
    public sealed class PickSkillModalView : GameModalView
    {
        [SerializeField] private RectTransform shopView = default;
        [SerializeField] private RectTransform statusView = default;
        [SerializeField] private CommonButtonView nextBattleButton = default;

        public override async UniTask ShowAsync(float duration, CancellationToken token)
        {
            await Show(duration).WithCancellation(token);
            await nextBattleButton.OnClickAsync(token);
        }

        public override Tween Show(float duration)
        {
            return DOTween.Sequence()
                .AppendCallback(() => canvasGroup.blocksRaycasts = true)
                .Append(canvasGroup
                    .DOFade(1.0f, duration)
                    .SetEase(Ease.OutBack))
                .Join(shopView
                    .DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBack))
                .Join(shopView
                    .DOAnchorPos(new Vector2(450.0f, 75.0f), duration)
                    .SetEase(Ease.OutBack))
                .Join(statusView
                    .DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBack))
                .Join(statusView
                    .DOAnchorPos(new Vector2(-450.0f, 75.0f), duration)
                    .SetEase(Ease.OutBack));
        }

        public override Tween Hide(float duration)
        {
            return DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(0.0f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(shopView
                    .DOScale(Vector3.one * 0.5f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(shopView
                    .DOAnchorPos(new Vector2(115.0f, 105.0f), duration)
                    .SetEase(Ease.OutQuart))
                .Join(statusView
                    .DOScale(Vector3.one * 0.5f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(statusView
                    .DOAnchorPos(new Vector2(-115.0f, 105.0f), duration)
                    .SetEase(Ease.OutQuart))
                .AppendCallback(() => canvasGroup.blocksRaycasts = false);
        }
    }
}