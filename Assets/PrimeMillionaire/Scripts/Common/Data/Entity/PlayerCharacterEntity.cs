namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class PlayerCharacterEntity
    {
        private CharacterType _type;

        public CharacterType type => _type;

        public void Reset()
        {
            _type = CharacterType.None;
        }

        public void SetType(CharacterType value)
        {
            if (value == CharacterType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            _type = value;
        }
    }
}