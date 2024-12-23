using DG.Tweening;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Image main = default;
        [SerializeField] private Image background = default;

        public void Render(CardVO card)
        {
            main.sprite = card.sprite;
        }

        public Tween TweenX(float value, float duration)
        {
            return transform.ToRectTransform()
                .DOAnchorPosX(value, duration);
        }
        
        public Tween TweenY(float value, float duration)
        {
            return transform.ToRectTransform()
                .DOAnchorPosY(value, duration);
        }
    }
}