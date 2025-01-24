using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DollarView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dollarValue = default;

        public void Render(int prev, int current)
        {
            DOTween.To(
                    () => prev,
                    x => dollarValue.text = $"{x:N0}",
                    current,
                    DollarConfig.TWEEN_DURATION
                )
                .SetLink(gameObject);
        }
    }
}