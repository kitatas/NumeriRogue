using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int type, int stage)
        {
            Type = type;
            Stage = stage;
        }

        [PrimaryKey] public int Type { get; }
        public int Stage { get; }

        public CharacterVO ToVO() => new(Type, Stage);
    }
}