using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(PrimeNumberMaster)), MessagePackObject(true)]
    public sealed class PrimeNumberMaster
    {
        public PrimeNumberMaster(int value)
        {
            Value = value;
        }

        [PrimaryKey] public int Value { get; }
    }
}