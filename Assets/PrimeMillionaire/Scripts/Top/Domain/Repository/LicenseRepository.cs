using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Top.Domain.Repository
{
    public sealed class LicenseRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public LicenseRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public IEnumerable<LicenseVO> GetAll()
        {
            return _memoryDatabase.LicenseMasterTable.All
                .Select(x => x.ToVO());
        }
    }
}