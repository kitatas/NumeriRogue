using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmData), menuName = "DataTable/" + nameof(BgmData))]
    public sealed class BgmData : ScriptableObject
    {
        [SerializeField] private Bgm bgmType = default;
        [SerializeField] private AudioClip audioClip = default;

        public Bgm bgm => bgmType;
        public AudioClip clip => audioClip;
    }
}