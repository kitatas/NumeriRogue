using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(DropRateMaster)), MessagePackObject(true)]
    public sealed class DropRateMaster
    {
        public DropRateMaster(int turn, float rate)
        {
            Turn = turn;
            Rate = rate;
        }

        [PrimaryKey] public int Turn { get; }
        public float Rate { get; }
    }
}