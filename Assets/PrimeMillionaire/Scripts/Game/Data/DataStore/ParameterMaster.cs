using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Game.Data.DataStore
{
    [MemoryTable(nameof(ParameterMaster)), MessagePackObject(true)]
    public sealed class ParameterMaster
    {
        public ParameterMaster(int type, int hp, int atk, int def)
        {
            Type = type;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        [PrimaryKey] public int Type { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }

        public ParameterVO ToVO() => new ParameterVO(Type, Hp, Atk, Def);
    }
}