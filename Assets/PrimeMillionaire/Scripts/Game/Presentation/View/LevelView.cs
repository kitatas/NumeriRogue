using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class LevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelValue = default;

        public void Render(int value)
        {
            levelValue.text = ZString.Format("{0}", value);
        }
    }
}