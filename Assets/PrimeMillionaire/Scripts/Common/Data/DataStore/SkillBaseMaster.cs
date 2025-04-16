using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillBaseMaster)), MessagePackObject(true)]
    public sealed class SkillBaseMaster
    {
        public SkillBaseMaster(int type, int target, int priceRate, string description)
        {
            Type = type;
            Target = target;
            PriceRate = priceRate;
            Description = description;
        }

        [PrimaryKey] public int Type { get; }
        public int Target { get; }
        public int PriceRate { get; }
        public string Description { get; }

        public SkillBaseVO ToVO() => new(Type, Target, PriceRate, Description);
    }
}