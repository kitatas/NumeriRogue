using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int type, int stage, int hp, int atk, int def)
        {
            Type = type;
            Stage = stage;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        [PrimaryKey] public int Type { get; }
        public int Stage { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }

        public CharacterVO ToVO() => new(Type, Stage, Hp, Atk, Def);
    }
}