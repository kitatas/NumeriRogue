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
            Suit.Club,
            Suit.Diamond,
            Suit.Heart,
            Suit.Spade,
        };

        public const float ROTATE_SPEED = 0.25f;
    }

    public sealed class OrderConfig
    {
        public const float TWEEN_DURATION = 0.25f;
    }

    public sealed class HandConfig
    {
        public const int MAX_NUM = 12;
        public const int ORDER_NUM = 3;

        public const float HAND_INTERVAL = 140.0f;
        public const float TWEEN_DURATION = 0.5f;
        public const float TRASH_DURATION = 0.05f;
    }
}