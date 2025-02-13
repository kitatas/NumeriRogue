using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class FailView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI failText = default;

        public void Init()
        {
            failText.SetColorA(0.0f);
        }

        public Tween FadeIn(float duration)
        {
            var textAnimation = new DOTweenTMPAnimator(failText);
            var charCount = textAnimation.textInfo.characterCount;
            var offset = Vector3.up * 40.0f;

            var tweens = new List<Tween>(charCount);
            for (int i = 0; i < charCount; i++)
            {
                tweens.Add(DOTween.Sequence()
                    .Append(textAnimation
                        .DOOffsetChar(i, textAnimation.GetCharOffset(i) + offset, duration)
                        .SetEase(Ease.OutFlash, 2))
                    .Join(textAnimation
                        .DOFadeChar(i, 1.0f, duration))
                    .SetDelay(i * 0.04f));
            }

            return tweens.Last();
        }
    }
}