using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class TurnView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI turnValue = default;

        public void Render(int value)
        {
            turnValue.text = $"{value}";
        }
    }
}