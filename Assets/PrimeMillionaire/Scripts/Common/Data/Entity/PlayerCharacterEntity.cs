using System;
using FastEnumUtility;

namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class PlayerCharacterEntity
    {
        private CharacterType _type;

        public CharacterType type => _type;

        public void SetType(CharacterType value)
        {
            if (value == CharacterType.None) throw new Exception();
            _type = value;
        }

        public int typeToInt => type.ToInt32();
    }
}