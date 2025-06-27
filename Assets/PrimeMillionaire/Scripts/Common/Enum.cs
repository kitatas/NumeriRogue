namespace PrimeMillionaire.Common
{
    public enum ButtonType
    {
        None = 0,
        Decision = 1,
        Cancel = 2,
    }

    public enum SceneName
    {
        None = 0,
        Boot = 1,
        Top = 2,
        Game = 3,
    }

    public enum LoadType
    {
        None = 0,
        Direct = 1,
        Fade = 2,
    }

    public enum Bgm
    {
        None = 0,
    }

    public enum Se
    {
        None = 0,
    }

    public enum Suit
    {
        None = 0,
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4,
    }

    public enum BonusType
    {
        None = 0,
        ValueDown = 1,
        OnePair = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        Flush = 5,
        PrimeNumber = 6,
        PowerOfTwo = 7,
        PerfectNumber = 8,
        PalindromicNumber = 9,
        SquareNumber = 10,
        TriangularNumber = 11,
        FibonacciNumber = 12,
        HarshadNumber = 13,
        MersenneNumber = 14,
    }

    public enum CharacterType
    {
        None = 0,
        Andromeda = 1,
        Borealjuggernaut = 2,
        Dissonance = 3,
        Kron = 4,
        Paragon = 5,
        Harmony = 6,
        Candypanda = 7,
        Chaosknight = 8,
        General = 9,
    }

    public enum ProgressStatus
    {
        New = 0,
        None = 1,
        Clear = 2,
    }

    public enum SkillTarget
    {
        None = 0,
        Atk = 1,
        Def = 2,
        Dollar = 3,
        Heal = 4,
        AtkDef = 5,
    }

    public enum SkillType
    {
        None = 0,
        Odd = 1,
        OddAtk = 2,
        OddDef = 3,
        Even = 4,
        EvenAtk = 5,
        EvenDef = 6,
        SuitMatch = 7,
        SuitMatchAtk = 8,
        SuitMatchDef = 9,
        SuitUnmatch = 10,
        SuitUnmatchAtk = 11,
        SuitUnmatchDef = 12,
        SuitMatchClub = 13,
        SuitMatchDiamond = 14,
        SuitMatchHeart = 15,
        SuitMatchSpade = 16,
        PrimeNumber = 17,
        PrimeNumberAtk = 18,
        PrimeNumberDef = 19,
        PrimeNumberDollar = 20,
        PrimeNumberHeal = 21,
        NotPrimeNumber = 22,
        NotPrimeNumberAtk = 23,
        NotPrimeNumberDef = 24,
        SameNumbers = 25,
        SameNumbersAtk = 26,
        SameNumbersDef = 27,
        SameNumbersDollar = 28,
        SameNumbersHeal = 29,
        NotSameNumbers = 30,
        NotSameNumbersAtk = 31,
        NotSameNumbersDef = 32,
        ValueDownDollar = 33,
        ValueDownHeal = 34,
        ValueUpAtk = 35,
        ValueUpDef = 36,
    }

    public enum StageType
    {
        None = 0,
        Abyssian = 1,
        Redrock = 2,
        Shimzar = 3,
        Vanar = 4,
    }
}