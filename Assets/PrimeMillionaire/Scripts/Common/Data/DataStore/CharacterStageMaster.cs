using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterStageMaster)), MessagePackObject(true)]
    public sealed class CharacterStageMaster
    {
        public CharacterStageMaster(int type, int[] suits, int[] ranks, int[] releaseConditions, int stage)
        {
            Type = type;
            Suits = suits;
            Ranks = ranks;
            ReleaseConditions = releaseConditions;
            Stage = stage;
        }

        [PrimaryKey] public int Type { get; }
        public int[] Suits { get; }
        public int[] Ranks { get; }
        public int[] ReleaseConditions { get; }
        public int Stage { get; }
    }
}