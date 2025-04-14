using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(NumericMaster)), MessagePackObject(true)]
    public sealed class NumericMaster
    {
        public NumericMaster(int value, int target)
        {
            Value = value;
            Target = target;
        }

        [PrimaryKey, NonUnique] public int Value { get; }
        public int Target { get; }
    }
}