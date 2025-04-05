using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CardMaster)), MessagePackObject(true)]
    public sealed class CardMaster
    {
        public CardMaster(int suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        [PrimaryKey(keyOrder: 0)] public int Suit { get; }
        [PrimaryKey(keyOrder: 1)] public int Rank { get; }

        public CardVO ToVO() => new(Suit, Rank);
    }
}