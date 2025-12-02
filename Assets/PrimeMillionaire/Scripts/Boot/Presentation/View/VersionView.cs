using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI version = default;

        private void Awake()
        {
            version.text = ZString.Format("Ver.{0}", Application.version);
        }
    }
}