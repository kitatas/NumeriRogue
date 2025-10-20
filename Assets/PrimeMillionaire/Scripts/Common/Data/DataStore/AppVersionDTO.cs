namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class AppVersionDTO
    {
        public int major;
        public int minor;

        public AppVersionVO ToVO() => new(major, minor);
    }
}