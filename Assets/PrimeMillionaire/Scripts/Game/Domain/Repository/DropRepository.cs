using System;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class DropRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public DropRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public float FindDropRate(int turn)
        {
            if (_memoryDatabase.DropRateMasterTable.TryFindByTurn(turn, out var master))
            {
                return master.Rate;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}