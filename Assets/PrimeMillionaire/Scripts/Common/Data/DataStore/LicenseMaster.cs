using MasterMemory;
using MessagePack;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [MemoryTable(nameof(LicenseMaster)), MessagePackObject(true)]
    public sealed class LicenseMaster
    {
        public LicenseMaster(string title, string content)
        {
            Title = title;
            Content = content;
        }

        [PrimaryKey] public string Title { get; }
        public string Content { get; }

        public LicenseVO ToVO() => new(Title, Content);
    }
}