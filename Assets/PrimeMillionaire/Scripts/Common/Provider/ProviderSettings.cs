using UnityEngine;

namespace PrimeMillionaire.Common.Provider
{
    [CreateAssetMenu(fileName = nameof(ProviderSettings), menuName = "DataTable/" + nameof(ProviderSettings))]
    public sealed class ProviderSettings : ScriptableObject
    {
        [SerializeField] private SoundType soundType = default;

        public SoundType sound => soundType;
    }
}