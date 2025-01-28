using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class PickSkillView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private RectTransform statusView = default;
        [SerializeField] private SkillView[] skillViews = default;
        [SerializeField] private Button nextBattleButton = default;

        public Tween FadeIn(float duration)
        {
            return DOTween.Sequence()
                .AppendCallback(() => canvasGroup.blocksRaycasts = true)
                .Append(canvasGroup
                    .DOFade(1.0f, duration)
                    .SetEase(Ease.OutBack))
                .Join(canvasGroup.transform.ToRectTransform()
                    .DOScale(Vector3.one, duration)
                    .SetEase(Ease.OutBack))
                .Join(statusView
                    .DOScale(1.0f, duration)
                    .SetEase(Ease.OutBack))
                .Join(statusView
                    .DOAnchorPos(new Vector2(-450.0f, 0.0f), duration)
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
                .Join(statusView
                    .DOScale(0.5f, duration)
                    .SetEase(Ease.OutQuart))
                .Join(statusView
                    .DOAnchorPos(new Vector2(-185.0f, 105.0f), duration)
                    .SetEase(Ease.OutQuart))
                .AppendCallback(() => canvasGroup.blocksRaycasts = false);
        }

        public async UniTask RenderAsync(PickSkillVO pickSkill, CancellationToken token)
        {
            await skillViews[pickSkill.index].RenderAsync(pickSkill.skill, token);
        }

        public async UniTask OnClickNextBattle(CancellationToken token)
        {
            await nextBattleButton.OnClickAsync(token);
        }
    }
}