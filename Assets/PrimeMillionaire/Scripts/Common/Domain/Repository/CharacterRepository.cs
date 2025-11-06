using System.Linq;
using PrimeMillionaire.Common.Data.DataStore;
using UniEx;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public CharacterRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public CharacterVO FindOther(int id)
        {
            return _memoryDbData.Get().CharacterMasterTable.All
                .Where(x => x.Id != id)
                .ToList()
                .GetRandom()
                .ToVO();
        }

        public CharacterVO Find(int id)
        {
            if (_memoryDbData.Get().CharacterMasterTable.TryFindById(id, out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            }
        }
    }
}