using Cysharp.Text;
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
            version.text = ZString.Format("ver: {0}", ZString.Join(".",
                VersionConfig.MAJOR_VERSION,
                VersionConfig.MINOR_VERSION
            ));
        }
    }
}