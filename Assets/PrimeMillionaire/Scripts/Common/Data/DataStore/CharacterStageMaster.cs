using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(CharacterStageMaster)), MessagePackObject(true)]
    public sealed class CharacterStageMaster
    {
        public CharacterStageMaster(int id, int[] suits, int[] ranks, int[] releaseConditions, int stage, int maxEnemyCount)
        {
            Id = id;
            Suits = suits;
            Ranks = ranks;
            ReleaseConditions = releaseConditions;
            Stage = stage;
            MaxEnemyCount = maxEnemyCount;
        }

        [PrimaryKey] public int Id { get; }
        public int[] Suits { get; }
        public int[] Ranks { get; }
        public int[] ReleaseConditions { get; }
        public int Stage { get; }
        public int MaxEnemyCount { get; }

        public StageVO ToStageVO() => new(Stage, MaxEnemyCount);
    }
}