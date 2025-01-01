using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BattlePtView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI player = default;
        [SerializeField] private TextMeshProUGUI enemy = default;

        public void RenderPlayer(int value)
        {
            DOTween.To(
                () => int.Parse(player.text),
                x => player.text = $"{x}",
                value,
                OrderConfig.TWEEN_DURATION);
        }

        public void RenderEnemy(int value)
        {
            DOTween.To(
                () => int.Parse(enemy.text),
                x => enemy.text = $"{x}",
                value,
                OrderConfig.TWEEN_DURATION);
        }
    }
}