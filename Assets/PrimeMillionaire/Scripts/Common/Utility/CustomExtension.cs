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
                SkillType.Odd => $"ATK & DEF +{value}%\n per odd number",
                SkillType.OddAtk => $"ATK +{value}%\n per odd number",
                SkillType.OddDef => $"DEF +{value}%\n per odd number",
                SkillType.Even => $"ATK & DEF +{value}%\n per even number",
                SkillType.EvenAtk => $"ATK +{value}%\n per even number",
                SkillType.EvenDef => $"DEF +{value}%\n per even number",
                SkillType.SuitMatch => $"ATK & DEF +{value}%\n per suit match",
                SkillType.SuitMatchAtk => $"ATK +{value}%\n per suit match",
                SkillType.SuitMatchDef => $"DEF +{value}%\n per suit match",
                SkillType.SuitUnmatch => $"ATK & DEF +{value}%\n per suit unmatch",
                SkillType.SuitUnmatchAtk => $"ATK +{value}%\n per suit unmatch",
                SkillType.SuitUnmatchDef => $"DEF +{value}%\n per suit unmatch",
                SkillType.SuitMatchClub => $"DEF +{value}%\n per club suit match",
                SkillType.SuitMatchDiamond => $"Get ${value}\n per diamond suit match",
                SkillType.SuitMatchHeart => $"Heal {value}\n per heart suit match",
                SkillType.SuitMatchSpade => $"ATK +{value}%\n per spade suit match",
                SkillType.PrimeNumber => $"ATK & DEF +{value}%\n per prime number",
                SkillType.PrimeNumberAtk => $"ATK +{value}%\n per prime number",
                SkillType.PrimeNumberDef => $"DEF +{value}%\n per prime number",
                SkillType.PrimeNumberDollar => $"Get ${value}\n per prime number",
                SkillType.PrimeNumberHeal => $"Heal {value}\n per prime number",
                SkillType.NotPrimeNumber => $"ATK & DEF +{value}%\n per not prime number",
                SkillType.NotPrimeNumberAtk => $"ATK +{value}%\n per not prime number",
                SkillType.NotPrimeNumberDef => $"DEF +{value}%\n per not prime number",
                SkillType.SameNumbers => $"ATK & DEF +{value}%\n per same numbers",
                SkillType.SameNumbersAtk => $"ATK +{value}%\n per same numbers",
                SkillType.SameNumbersDef => $"DEF +{value}%\n per same numbers",
                SkillType.SameNumbersDollar => $"Get ${value}\n per same numbers",
                SkillType.SameNumbersHeal => $"Heal {value}\n per same numbers",
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SKILL_DESCRIPTION),
            };
        }

        public static int ToPrice(this SkillType self, int value)
        {
            return self switch
            {
                SkillType.Odd => value * 5,
                SkillType.OddAtk => value * 5,
                SkillType.OddDef => value * 5,
                SkillType.Even => value * 5,
                SkillType.EvenAtk => value * 5,
                SkillType.EvenDef => value * 5,
                SkillType.SuitMatch => value * 5,
                SkillType.SuitMatchAtk => value * 5,
                SkillType.SuitMatchDef => value * 5,
                SkillType.SuitUnmatch => value * 5,
                SkillType.SuitUnmatchAtk => value * 5,
                SkillType.SuitUnmatchDef => value * 5,
                SkillType.SuitMatchClub => value * 5,
                SkillType.SuitMatchDiamond => value * 5,
                SkillType.SuitMatchHeart => value * 5,
                SkillType.SuitMatchSpade => value * 5,
                SkillType.PrimeNumber => value * 5,
                SkillType.PrimeNumberAtk => value * 5,
                SkillType.PrimeNumberDef => value * 5,
                SkillType.PrimeNumberDollar => value * 5,
                SkillType.PrimeNumberHeal => value * 5,
                SkillType.NotPrimeNumber => value * 5,
                SkillType.NotPrimeNumberAtk => value * 5,
                SkillType.NotPrimeNumberDef => value * 5,
                SkillType.SameNumbers => value * 5,
                SkillType.SameNumbersAtk => value * 5,
                SkillType.SameNumbersDef => value * 5,
                SkillType.SameNumbersDollar => value * 5,
                SkillType.SameNumbersHeal => value * 5,
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