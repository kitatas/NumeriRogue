using Cysharp.Text;
using DG.Tweening;
using PrimeMillionaire.Common;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BattlePtView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI player = default;
        [SerializeField] private TextMeshProUGUI enemy = default;

        public Tween RenderPlayer(int value)
        {
            return DOTween.To(
                () => int.Parse(player.text),
                x => player.text = ZString.Format("{0}", x),
                value,
                UiConfig.TWEEN_DURATION);
        }

        public Tween RenderEnemy(int value)
        {
            return DOTween.To(
                () => int.Parse(enemy.text),
                x => enemy.text = ZString.Format("{0}", x),
                value,
                UiConfig.TWEEN_DURATION);
        }
    }
}