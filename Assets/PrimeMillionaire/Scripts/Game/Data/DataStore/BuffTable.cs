using System.Collections.Generic;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BuffTable), menuName = "DataTable/" + nameof(BuffTable))]
    public sealed class BuffTable : ScriptableObject
    {
        [SerializeField] private List<BuffData> dataList = default;

        public List<BuffData> list => dataList;
    }
}