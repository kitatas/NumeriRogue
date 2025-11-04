namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class MemoryDbData
    {
        private MemoryDatabase _memoryDatabase;

        public MemoryDbData(MemoryDatabase memoryDatabase)
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