using System.Collections.Generic;
using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable))]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> list = default;

        public List<SeData> all => list;
    }
}