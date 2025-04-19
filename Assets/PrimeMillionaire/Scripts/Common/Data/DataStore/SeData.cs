using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeData), menuName = "DataTable/" + nameof(SeData))]
    public sealed class SeData : ScriptableObject
    {
        [SerializeField] private Se seType = default;
        [SerializeField] private AudioClip audioClip = default;

        public Se se => seType;
        public AudioClip clip => audioClip;
    }
}