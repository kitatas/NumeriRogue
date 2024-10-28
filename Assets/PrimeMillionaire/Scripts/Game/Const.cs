namespace PrimeMillionaire.Game
{
    public sealed class GameConfig
    {
        public const GameState INIT_STATE = GameState.Init;
    }

    public sealed class CardConfig
    {
        public const int MAX_RANK = 13;

        public static readonly Suit[] SUITS =
        {
            Suit.Clover,
            Suit.Diamond,
            Suit.Heart,
            Suit.Spade,
        };
    }
}