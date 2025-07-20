using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(NumericBonusMaster)), MessagePackObject(true)]
    public sealed class NumericBonusMaster
    {
        public NumericBonusMaster(int type, int rate, int[] skillTypes)
        {
            Type = type;
            Rate = rate;
            SkillTypes = skillTypes;
        }

        [PrimaryKey] public int Type { get; }
        public int Rate { get; }
        public int[] SkillTypes { get; }

        public BonusVO ToVO() => new(Type, Rate);
        public BonusTargetVO ToTargetVO() => new (Type, SkillTypes); 
    }
}