using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(DropRateMaster)), MessagePackObject(true)]
    public sealed class DropRateMaster
    {
        public DropRateMaster(int turn, int rate)
        {
            Turn = turn;
            Rate = rate;
        }

        [PrimaryKey] public int Turn { get; }
        public int Rate { get; }

        public DropRateVO ToVO() => new(Turn, Rate);
    }
}