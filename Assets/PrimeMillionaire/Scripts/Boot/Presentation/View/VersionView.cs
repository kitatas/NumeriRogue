using PrimeMillionaire.Common;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI version = default;

        private void Awake()
        {
            version.text = $"ver: {VersionConfig.APP_VERSION}";
        }
    }
}