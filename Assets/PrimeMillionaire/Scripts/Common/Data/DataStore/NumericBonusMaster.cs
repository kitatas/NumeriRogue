using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(NumericBonusMaster)), MessagePackObject(true)]
    public sealed class NumericBonusMaster
    {
        public NumericBonusMaster(int type, int rate)
        {
            Type = type;
            Rate = rate;
        }

        [PrimaryKey] public int Type { get; }
        public int Rate { get; }

        public BonusVO ToVO() => new(Type, Rate);
    }
}