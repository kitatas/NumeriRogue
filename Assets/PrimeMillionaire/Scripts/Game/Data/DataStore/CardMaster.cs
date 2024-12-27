using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CardMaster)), MessagePackObject(true)]
    public sealed class CardMaster
    {
        public CardMaster(int id, Suit suit, int rank, string imgPath)
        {
            this.id = id;
            this.suit = suit;
            this.rank = rank;
            this.imgPath = imgPath;
        }

        [PrimaryKey] public int id { get; }
        public Suit suit { get; }
        public int rank { get; }
        public string imgPath { get; }

        [IgnoreMember] public CardVO card => new(suit, rank, imgPath);
    }
}