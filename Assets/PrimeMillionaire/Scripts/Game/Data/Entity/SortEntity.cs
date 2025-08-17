using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class SortEntity
    {
        public Sort value { get; private set; }

        public SortEntity()
        {
            value = Sort.Rank;
        }
    }
}