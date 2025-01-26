using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class PickSkillView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private SkillView[] skillViews = default;

        public Tween FadeIn(float duration)
        {
            return DOTween.Sequence()
                .AppendCallback(() => canvasGroup.blocksRaycasts = true)
                .Append(canvasGroup
                    .DOFade(1.0f, duration)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBack));
        }

        public Tween FadeOut(float duration)
        {
            return DOTween.Sequence()
                .Append(canvasGroup
                    .DOFade(0.0f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one * 0.8f, duration)
                    .SetEase(Ease.OutQuart))
                .AppendCallback(() => canvasGroup.blocksRaycasts = false);
        }

        public async UniTask RenderAsync(PickSkillVO pickSkill, CancellationToken token)
        {
            await skillViews[pickSkill.index].RenderAsync(pickSkill.skill, token);
        }

        public List<UniTask<SkillVO>> OnClicksAsync(CancellationToken token)
        {
            return skillViews
                .Select(x => x.SelectAsync(token))
                .ToList();
        }
    }
}