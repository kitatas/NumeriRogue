using System;
using System.Collections.Generic;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Top
{
    public sealed class ModalVO : BaseModalVO<ModalType>
    {
        public ModalVO(ModalType type, bool isActivate) : base(type, isActivate)
        {
        }
    }

    public sealed class StageCharacterVO
    {
        public readonly CharacterVO character;
        public readonly CharacterProgressVO progress;

        public StageCharacterVO(CharacterVO character, CharacterProgressVO progress)
        {
            this.character = character;
            this.progress = progress;
        }
    }

    public sealed class OrderCharacterVO
    {
        public readonly CharacterVO character;
        public readonly IEnumerable<CardVO> deck;

        public OrderCharacterVO(CharacterVO character, IEnumerable<CardVO> deck)
        {
            this.character = character;
            this.deck = deck;
        }
    }

    public sealed class ScrollContextVO
    {
        public int index = -1;
        public Action<int> onSelect;
    }
}