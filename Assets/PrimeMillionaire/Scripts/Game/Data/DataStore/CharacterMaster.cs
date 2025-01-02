using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(CharacterType type, string objPath)
        {
            this.type = type;
            this.objPath = objPath;
        }

        [PrimaryKey] public CharacterType type { get; }
        public string objPath { get; }

        [IgnoreMember] public CharacterVO character => new CharacterVO(type, objPath);
    }
}