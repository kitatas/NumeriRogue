using System.Collections.Generic;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(CardTable), menuName = "DataTable/" + nameof(CardTable))]
    public sealed class CardTable : ScriptableObject
    {
        [SerializeField] private List<CardData> data = default;

        public List<CardData> records => data;
    }
}