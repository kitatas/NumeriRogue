namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class MemoryData
    {
        private MemoryDatabase _memoryDatabase;

        public MemoryData(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void Set(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public MemoryDatabase Get()
        {
            return _memoryDatabase;
        }
    }
}