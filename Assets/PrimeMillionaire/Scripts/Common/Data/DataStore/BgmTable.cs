using System.Collections.Generic;
using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmTable), menuName = "DataTable/" + nameof(BgmTable))]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private List<BgmData> list = default;

        public List<BgmData> all => list;
    }
}