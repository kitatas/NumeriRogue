using UnityEngine;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(CardData), menuName = "DataTable/" + nameof(CardData))]
    public sealed class CardData : ScriptableObject
    {
        [SerializeField] private Suit suit = default;
        [SerializeField, Range(1, 13)] private int rank = default;
        [SerializeField] private Sprite sprite = default;

        public CardVO card => new CardVO(suit, rank, sprite);
    }
}