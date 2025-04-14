using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(NumericMaster)), MessagePackObject(true)]
    public sealed class NumericMaster
    {
        public NumericMaster(int value, int bonus)
        {
            Value = value;
            Bonus = bonus;
        }

        [PrimaryKey, NonUnique] public int Value { get; }
        public int Bonus { get; }
    }
}