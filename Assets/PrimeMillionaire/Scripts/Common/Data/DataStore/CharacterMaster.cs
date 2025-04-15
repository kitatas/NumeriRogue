using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int type, int hp, int atk, int def, int[] releaseConditions)
        {
            Type = type;
            Hp = hp;
            Atk = atk;
            Def = def;
            ReleaseConditions = releaseConditions;
        }

        [PrimaryKey] public int Type { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }
        public int[] ReleaseConditions { get; }

        public CharacterVO ToVO() => new(Type, Hp, Atk, Def);
    }
}