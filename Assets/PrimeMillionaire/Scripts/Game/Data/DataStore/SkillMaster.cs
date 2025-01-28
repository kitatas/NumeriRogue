using MasterMemory;
using MessagePack;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(SkillMaster)), MessagePackObject(true)]
    public sealed class SkillMaster
    {
        public SkillMaster(int id, int level, int type, int min, int max)
        {
            Id = id;
            Level = level;
            Type = type;
            Min = min;
            Max = max;
        }

        [PrimaryKey] public int Id { get; }
        [SecondaryKey(0), NonUnique] public int Level { get; }
        public int Type { get; }
        public int Min { get; }
        public int Max { get; }

        public SkillVO ToVO() => new(Type, Random.Range(Min, Max));
    }
}