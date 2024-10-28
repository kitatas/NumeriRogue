namespace PrimeMillionaire.Game
{
    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;

        public CardVO(Suit suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
    }
}