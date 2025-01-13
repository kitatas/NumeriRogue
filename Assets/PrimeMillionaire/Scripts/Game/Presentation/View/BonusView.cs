using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FastEnumUtility;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BonusView : MonoBehaviour
    {
        [SerializeField] private Image background = default;
        [SerializeField] private TextMeshProUGUI bonusType = default;
        [SerializeField] private TextMeshProUGUI bonusValue = default;

        public async UniTask TweenAsync(BonusType type, CancellationToken token)
        {
            bonusType.text = $"{type.FastToString()}";
            bonusValue.text = $"x {type.ToBonus()}";

            await Show(0.25f).WithCancellation(token);
            await UniTaskHelper.DelayAsync(0.25f, token);
            Hide(0.25f).WithCancellation(token).Forget();
        }

        public Tween Show(float duration)
        {
            var backgroundY = background.rectTransform.anchoredPosition.y;
            return DOTween.Sequence()
                .Append(background.DOFade(1.0f, duration))
                .Join(bonusType.DOFade(1.0f, duration))
                .Join(bonusValue.DOFade(1.0f, duration))
                .Join(background.rectTransform.DOAnchorPosY(backgroundY + 45.0f, duration));
        }

        public Tween Hide(float duration)
        {
            var backgroundY = background.rectTransform.anchoredPosition.y;
            return DOTween.Sequence()
                .Append(background.DOFade(0.0f, duration))
                .Join(bonusType.DOFade(0.0f, duration))
                .Join(bonusValue.DOFade(0.0f, duration))
                .Join(background.rectTransform.DOAnchorPosY(backgroundY + 45.0f, duration))
                .Append(background.rectTransform.DOAnchorPosY(backgroundY - 45.0f, 0.0f));
        }
    }
}