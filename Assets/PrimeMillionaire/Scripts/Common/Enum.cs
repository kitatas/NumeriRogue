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
        ValueUp = 1,
        ValueDown = 2,
        HighCard = 3,
        OnePair = 4,
        Flush = 5,
        Straight = 6,
        ThreeOfAKind = 7,
        StraightFlush = 8,
        PrimeNumber = 9,
        PowerOfTwo = 10,
        PerfectNumber = 11,
        PalindromicNumber = 12,
        SquareNumber = 13,
        TriangularNumber = 14,
        FibonacciNumber = 15,
        HarshadNumber = 16,
        MersenneNumber = 17,
    }

    public enum CharacterType
    {
        None = 0,
        General = 1,

        Borealjuggernaut = 2,
        Dissonance = 3,
        Kron = 4,
        Paragon = 5,
        Harmony = 6,
        Candypanda = 7,
        Chaosknight = 8,
        Andromeda = 9,
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
        ValueUpAtk = 1,
        ValueUpDef = 2,
        ValueDownDollar = 3,
        ValueDownHeal = 4,
        HighCard = 5,
        HighCardAtk = 6,
        HighCardDef = 7,
        PokerHands = 8,
        PokerHandsAtk = 9,
        PokerHandsDef = 10,
        OnePair = 11,
        Flush = 12,
        Straight = 13,
        ThreeOfAKind = 14,
        StraightFlush = 15,
        Odd = 16,
        OddAtk = 17,
        OddDef = 18,
        Even = 19,
        EvenAtk = 20,
        EvenDef = 21,
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