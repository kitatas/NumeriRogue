using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int type, string objPath)
        {
            Type = type;
            ObjPath = objPath;
        }

        [PrimaryKey] public int Type { get; }
        public string ObjPath { get; }

        public CharacterVO ToVO() => new(Type, ObjPath);
    }
}