using System.Collections.Generic;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Top
{
    public sealed class OrderCharacterVO
    {
        public readonly CharacterVO character;
        public readonly ParameterVO parameter;
        public readonly IEnumerable<CardVO> deck;

        public OrderCharacterVO(CharacterVO character, ParameterVO parameter, IEnumerable<CardVO> deck)
        {
            this.character = character;
            this.parameter = parameter;
            this.deck = deck;
        }
    }
}