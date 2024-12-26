using UnityEngine;

namespace PrimeMillionaire.Game
{
    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;
        public readonly string imgPath;
        public readonly Sprite sprite;

        public CardVO(Suit suit, int rank, Sprite sprite)
        {
            this.suit = suit;
            this.rank = rank;
            this.sprite = sprite;
        }

        public CardVO(Suit suit, int rank, string imgPath)
        {
            this.suit = suit;
            this.rank = rank;
            this.imgPath = imgPath;
        }
    }

    public sealed class HandVO
    {
        public readonly int index;
        public readonly CardVO cardVO;

        public HandVO(int index, CardVO cardVO)
        {
            this.index = index;
            this.cardVO = cardVO;
        }
    }
}