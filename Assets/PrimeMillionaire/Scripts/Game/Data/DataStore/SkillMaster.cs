using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(SkillMaster)), MessagePackObject(true)]
    public sealed class SkillMaster
    {
        public SkillMaster(int id, int level, int type, int value)
        {
            Id = id;
            Level = level;
            Type = type;
            Value = value;
        }

        [PrimaryKey] public int Id { get; }
        [SecondaryKey(0), NonUnique] public int Level { get; }
        public int Type { get; }
        public int Value { get; }

        public SkillVO ToVO() => new(Type, Value);
    }
}