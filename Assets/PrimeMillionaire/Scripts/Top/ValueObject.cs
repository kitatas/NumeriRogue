using PrimeMillionaire.Common;

namespace PrimeMillionaire.Top
{
    public sealed class OrderCharacterVO
    {
        public readonly CharacterVO character;
        public readonly ParameterVO parameter;

        public OrderCharacterVO(CharacterVO character, ParameterVO parameter)
        {
            this.character = character;
            this.parameter = parameter;
        }
    }
}