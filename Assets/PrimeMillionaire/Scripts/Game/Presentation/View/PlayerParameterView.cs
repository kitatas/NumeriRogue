using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class PlayerParameterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName = default;
        [SerializeField] private TextMeshProUGUI hp = default;
        [SerializeField] private TextMeshProUGUI atk = default;
        [SerializeField] private TextMeshProUGUI def = default;
        [SerializeField] private TextMeshProUGUI currentHp = default;

        public Tween Render(ParameterVO parameter, float duration)
        {
            characterName.text = $"{parameter.name}";

            return DOTween.Sequence()
                .Append(DOTween.To(
                    () => int.Parse(hp.text),
                    x => hp.text = $"{x}",
                    parameter.hp,
                    duration))
                .Join(DOTween.To(
                    () => int.Parse(atk.text),
                    x => atk.text = $"{x}",
                    parameter.atk,
                    duration))
                .Join(DOTween.To(
                    () => int.Parse(def.text),
                    x => def.text = $"{x}",
                    parameter.def,
                    duration))
                .Join(DOTween.To(
                    () => int.Parse(currentHp.text),
                    x => currentHp.text = $"{x}",
                    parameter.currentHp,
                    duration));
        }
    }
}