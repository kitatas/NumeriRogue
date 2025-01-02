using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CardMaster)), MessagePackObject(true)]
    public sealed class CardMaster
    {
        public CardMaster(int id, Suit suit, int rank, string imgPath)
        {
            Id = id;
            Suit = suit;
            Rank = rank;
            ImgPath = imgPath;
        }

        [PrimaryKey] public int Id { get; }
        public Suit Suit { get; }
        public int Rank { get; }
        public string ImgPath { get; }

        [IgnoreMember] public CardVO card => new(Suit, Rank, ImgPath);
    }
}