using System;
using FastEnumUtility;
using PrimeMillionaire.Game.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class ParameterRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public ParameterRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public ParameterVO Find(CharacterType type)
        {
            if (_memoryDatabase.ParameterMasterTable.TryFindByType(type.ToInt32(), out var master))
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