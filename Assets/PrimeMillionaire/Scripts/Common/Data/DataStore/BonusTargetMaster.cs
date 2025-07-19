using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(BonusTargetMaster)), MessagePackObject(true)]
    public sealed class BonusTargetMaster
    {
        public BonusTargetMaster(int type, int[] skillTypes)
        {
            Type = type;
            SkillTypes = skillTypes;
        }

        [PrimaryKey] public int Type { get; }
        public int[] SkillTypes { get; }

        public BonusTargetVO ToVO() => new(Type, SkillTypes);
    }
}