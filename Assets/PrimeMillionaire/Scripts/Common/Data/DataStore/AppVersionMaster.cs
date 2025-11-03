using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(AppVersionMaster)), MessagePackObject(true)]
    public sealed class AppVersionMaster
    {
        public AppVersionMaster(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        [PrimaryKey(keyOrder: 0)] public int Major { get; }
        [PrimaryKey(keyOrder: 1)] public int Minor { get; }

        public AppVersionVO ToVO() => new(Major, Minor);
    }
}