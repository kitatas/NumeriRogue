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

        public static SkillType ToSkillType(this int self)
        {
            return FastEnum.IsDefined<SkillType>(self)
                ? (SkillType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL);
        }

        public static string ToDescription(this SkillType self, int value)
        {
            return self switch
            {
                SkillType.OddAtk => $"ATK +{value}% per odd number",
                SkillType.OddDef => $"DEF +{value}% per odd number",
                SkillType.EvenAtk => $"ATK +{value}% per even number",
                SkillType.EvenDef => $"DEF +{value}% per even number",
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL_DESCRIPTION),
            };
        }

        public static int ToPrice(this SkillType self, int value)
        {
            return self switch
            {
                SkillType.OddAtk => value * 5,
                SkillType.OddDef => value * 5,
                SkillType.EvenAtk => value * 5,
                SkillType.EvenDef => value * 5,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL_PRICE),
            };
        }

        public static StageType ToStageType(this int self)
        {
            return FastEnum.IsDefined<StageType>(self)
                ? (StageType)self
                : throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_STAGE);
        }
    }
}