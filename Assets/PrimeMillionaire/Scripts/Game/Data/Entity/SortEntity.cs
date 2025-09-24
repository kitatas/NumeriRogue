using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class SortEntity
    {
        public Sort value { get; private set; }

        public SortEntity()
        {
            Set(HandConfig.INIT_SORT);
        }

        public void Set(Sort x)
        {
            if (x == Sort.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SORT);
            value = x;
        }
    }
}