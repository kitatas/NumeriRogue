using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(LevelMaster)), MessagePackObject(true)]
    public sealed class LevelMaster
    {
        public LevelMaster(int level, float rate)
        {
            Level = level;
            Rate = rate;
        }

        [PrimaryKey] public int Level { get; }
        public float Rate { get; }

        public LevelVO ToVO() => new(Level, Rate);
    }
}