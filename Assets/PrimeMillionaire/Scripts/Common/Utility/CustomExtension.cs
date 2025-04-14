using FastEnumUtility;

namespace PrimeMillionaire.Common.Utility
{
    public static class CustomExtension
    {
        public static Suit ToSuit(this int self)
        {
            return FastEnum.IsDefined<Suit>(self)
                ? (Suit)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SUIT);
        }

        public static CharacterType ToCharacterType(this int self)
        {
            return FastEnum.IsDefined<CharacterType>(self)
                ? (CharacterType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
        }

        public static BonusType ToBonusType(this int self)
        {
            return FastEnum.IsDefined<BonusType>(self)
                ? (BonusType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS);
        }

        public static SkillType ToSkillType(this int self)
        {
            return FastEnum.IsDefined<SkillType>(self)
                ? (SkillType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
        }

        public static StageType ToStageType(this int self)
        {
            return FastEnum.IsDefined<StageType>(self)
                ? (StageType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_STAGE);
        }
    }
}