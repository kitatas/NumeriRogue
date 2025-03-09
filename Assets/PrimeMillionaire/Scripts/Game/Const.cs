using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game
{
    public sealed class DebugConfig
    {
        public const Side IS_FORCE_WIN = Side.None;
    }

    public sealed class BattleConfig
    {
        public const float TWEEN_DURATION = 0.25f;
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

    public sealed class DollarConfig
    {
        public const float TWEEN_DURATION = 0.25f;
        public const int CLEAR_THRESHOLD = 1500;
    }

    public sealed class HandConfig
    {
        public const int MAX_NUM = DebugConfig.IS_FORCE_WIN == Side.None ? 9 : ORDER_NUM;
        public const int ORDER_NUM = 3;

        public const float HAND_INTERVAL = 130.0f;
        public const float TWEEN_DURATION = 0.25f;
        public const float TRASH_DURATION = 0.05f;
    }

    public sealed class CharacterConfig
    {
        public const float MOVE_TIME = 0.25f;
    }

    public sealed class SkillConfig
    {
        public const int LOT_NUM = 3;

        public const int HOLD_NUM = 5;
    }
}