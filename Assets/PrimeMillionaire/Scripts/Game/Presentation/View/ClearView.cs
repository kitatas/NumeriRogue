using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class ClearView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clearText = default;

        public void Init()
        {
            clearText.SetColorA(0.0f);
        }

        public Tween FadeIn(float duration)
        {
            var textAnimation = new DOTweenTMPAnimator(clearText);
            var charCount = textAnimation.textInfo.characterCount;
            var offset = Vector3.up * 40.0f;

            for (int i = 0; i < charCount; i++)
            {
                DOTween.Sequence()
                    .Append(textAnimation
                        .DOOffsetChar(i, textAnimation.GetCharOffset(i) + offset, duration)
                        .SetEase(Ease.OutFlash, 2))
                    .Join(textAnimation
                        .DOFadeChar(i, 1.0f, duration))
                    .SetDelay(i * 0.04f);
            }

            var tweens = new List<Tween>(charCount);
            var highlightColor = new Color(1f, 1f, 0.8f);
            var textInfo = clearText.textInfo;
            for (int i = 0; i < charCount; i++)
            {
                var interval = i * 0.05f + (charCount * 0.05f);
                var referenceIndex = textInfo.characterInfo[i].materialReferenceIndex;
                var vertexColors = textInfo.meshInfo[referenceIndex].colors32;
                var vertexIndex = textInfo.characterInfo[i].vertexIndex;
                tweens.Add(DOTween.Sequence()
                    .AppendInterval(interval)
                    .Append(textAnimation
                        .DOColorChar(i, highlightColor, 0.15f)
                        .SetEase(Ease.Linear))
                    .AppendCallback(() =>
                    {
                        vertexColors[vertexIndex + 0] = Color.yellow;
                        vertexColors[vertexIndex + 1] = Color.red;
                        vertexColors[vertexIndex + 2] = Color.red;
                        vertexColors[vertexIndex + 3] = Color.yellow;
                        clearText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                    })
                    .SetLink(gameObject));
            }

            return tweens.Last();
        }
    }
}