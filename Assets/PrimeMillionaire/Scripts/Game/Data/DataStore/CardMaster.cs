using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CardMaster)), MessagePackObject(true)]
    public sealed class CardMaster
    {
        public CardMaster(int id, int rank, Suit suit, string imgPath)
        {
            this.id = id;
            this.rank = rank;
            this.suit = suit;
            this.imgPath = imgPath;
        }

        [PrimaryKey] public int id { get; }
        public int rank { get; }
        public Suit suit { get; }
        public string imgPath { get; }
    }
}