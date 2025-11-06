using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterMaster)), MessagePackObject(true)]
    public sealed class CharacterMaster
    {
        public CharacterMaster(int id, string name, int hp, int atk, int def)
        {
            Id = id;
            Name = name;
            Hp = hp;
            Atk = atk;
            Def = def;
        }

        [PrimaryKey] public int Id { get; }
        public string Name { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }

        public CharacterVO ToVO() => new(Id, Name, Hp, Atk, Def);
    }
}