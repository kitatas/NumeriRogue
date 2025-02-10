using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CardMaster)), MessagePackObject(true)]
    public sealed class CardMaster
    {
        public CardMaster(int id, int suit, int rank)
        {
            Id = id;
            Suit = suit;
            Rank = rank;
        }

        [PrimaryKey] public int Id { get; }
        public int Suit { get; }
        public int Rank { get; }

        public CardVO ToVO() => new(Suit, Rank);
        [IgnoreMember] public CardVO card => new(Suit, Rank);
    }
}