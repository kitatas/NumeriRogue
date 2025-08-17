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

        public void Switch()
        {
            value = value switch
            {
                Sort.Rank => Sort.Suit,
                Sort.Suit => Sort.Rank,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SORT),
            };
        }
    }
}