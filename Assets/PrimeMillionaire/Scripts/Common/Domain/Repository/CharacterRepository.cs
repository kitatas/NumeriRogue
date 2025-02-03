using System;
using FastEnumUtility;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public CharacterRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public CharacterVO Find(CharacterType type)
        {
            if (_memoryDatabase.CharacterMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}