using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int type)
        {
            Type = type;
        }

        [PrimaryKey] public int Type { get; }

        public CharacterVO ToVO() => new(Type);
    }
}