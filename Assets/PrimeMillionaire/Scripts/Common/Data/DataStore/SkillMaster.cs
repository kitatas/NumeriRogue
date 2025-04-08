using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillMaster)), MessagePackObject(true)]
    public sealed class SkillMaster
    {
        public SkillMaster(int type, int priceRate, string description)
        {
            Type = type;
            PriceRate = priceRate;
            Description = description;
        }

        [PrimaryKey] public int Type { get; }
        public int PriceRate { get; }
        public string Description { get; }
    }
}