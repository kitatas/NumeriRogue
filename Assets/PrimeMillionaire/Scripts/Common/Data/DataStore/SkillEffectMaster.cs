using MasterMemory;
using MessagePack;
using UnityEngine;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillEffectMaster)), MessagePackObject(true)]
    public sealed class SkillEffectMaster
    {
        public SkillEffectMaster(int type, int level, int min, int max)
        {
            Type = type;
            Level = level;
            Min = min;
            Max = max;
        }

        [PrimaryKey, NonUnique] public int Type { get; }
        [SecondaryKey(0), NonUnique] public int Level { get; }
        public int Min { get; }
        public int Max { get; }

        public SkillVO ToVO() => new(Type, Random.Range(Min, Max));
    }
}