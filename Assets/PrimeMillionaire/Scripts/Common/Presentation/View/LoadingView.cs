using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class LoadingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loading = default;

        public void Activate(bool value)
        {
            loading.enabled = value;
        }
    }
}