using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(ParameterMaster)), MessagePackObject(true)]
    public sealed class ParameterMaster
    {
        public ParameterMaster(CharacterType type, int hp, int atk, int def)
        {
            Type = type;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        [PrimaryKey] public CharacterType Type { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }

        [IgnoreMember] public ParameterVO parameter => new ParameterVO(Type, Hp, Atk, Def);
    }
}