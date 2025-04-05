using MasterMemory;
using MessagePack;
using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillMaster)), MessagePackObject(true)]
    public sealed class SkillMaster
    {
        public SkillMaster(int level, int type, int min, int max)
        {
            Level = level;
            Type = type;
            Min = min;
            Max = max;
        }

        [PrimaryKey, NonUnique] public int Level { get; }
        public int Type { get; }
        public int Min { get; }
        public int Max { get; }

        public SkillVO ToVO() => new(Type, Random.Range(Min, Max));
    }
}