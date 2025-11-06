namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class PlayerCharacterEntity
    {
        private int _id;

        public int id => _id;

        public void Reset()
        {
            _id = 0;
        }

        public void SetType(int value)
        {
            if (value == 0) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            _id = value;
        }
    }
}