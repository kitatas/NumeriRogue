using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(SkillMaster)), MessagePackObject(true)]
    public sealed class SkillMaster
    {
        public SkillMaster(int type, string icon, int priceRate, string description)
        {
            Type = type;
            Icon = icon;
            PriceRate = priceRate;
            Description = description;
        }

        [PrimaryKey] public int Type { get; }
        public string Icon { get; }
        public int PriceRate { get; }
        public string Description { get; }
    }
}