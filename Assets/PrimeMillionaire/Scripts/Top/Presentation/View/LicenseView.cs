using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class LicenseView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI content = default;

        public void Render(string value)
        {
            content.text = value;
        }
    }
}