using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillTargetMaster)), MessagePackObject(true)]
    public sealed class SkillTargetMaster
    {
        public SkillTargetMaster(int target, int[] targets)
        {
            Target = target;
            Targets = targets;
        }

        [PrimaryKey] public int Target { get; }
        public int[] Targets { get; }
    }
}