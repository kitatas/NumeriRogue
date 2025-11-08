using System.Collections.Generic;
using UnityEngine;

namespace PrimeMillionaire.Boot.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SplashTable), menuName = "DataTable/" + nameof(SplashTable))]
    public sealed class SplashTable : ScriptableObject
    {
        [SerializeField] private List<SplashData> list = default;

        public List<SplashData> all => list;
    }
}