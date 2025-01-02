using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(CharacterType type, string objPath)
        {
            Type = type;
            ObjPath = objPath;
        }

        [PrimaryKey] public CharacterType Type { get; }
        public string ObjPath { get; }

        [IgnoreMember] public CharacterVO character => new CharacterVO(Type, ObjPath);
    }
}