using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(DeckMaster)), MessagePackObject(true)]
    public sealed class DeckMaster
    {
        public DeckMaster(int type, int[] suits, int[] ranks)
        {
            Type = type;
            Suits = suits;
            Ranks = ranks;
        }

        [PrimaryKey] public int Type { get; }
        public int[] Suits { get; }
        public int[] Ranks { get; }
    }
}