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
        Menu = 1,
        Game = 2,
    }

    public enum Se
    {
        None = 0,
        Decision = 1,
        Cancel = 2,
    }

    public enum Suit
    {
        None = 0,
        Spade = 1,
        Heart = 2,
        Club = 3,
        Diamond = 4,
    }

    public enum BonusType
    {
        None = 0,
        ValueUp = 1,
        ValueDown = 2,
        HighCard = 3,
        PokerHands = 4,
        OnePair = 5,
        Flush = 6,
        Straight = 7,
        ThreeOfAKind = 8,
        StraightFlush = 9,
        OddNumber = 10,
        EvenNumber = 11,
        NotSpecialNumbers = 12,
        SpecialNumbers = 13,
        PrimeNumber = 14,
        PowerOfTwo = 15,
        PerfectNumber = 16,
        PalindromicNumber = 17,
        SquareNumber = 18,
        TriangularNumber = 19,
        FibonacciNumber = 20,
        HarshadNumber = 21,
        MersenneNumber = 22,
        KaprekarNumber = 23,
        PellNumber = 24,
    }

    public enum PokerHands
    {
        HighCard = 0,
        OnePair = 1,
        Flush = 2,
        Straight = 3,
        ThreeOfAKind = 4,
        StraightFlush = 5,
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
        Crystal = 10,
        Antiswarm = 11,
        Christmas = 12,
        Cindera = 13,
        Decepticleprime = 14,
        Emp = 15,
        Grym = 16,
        Invader = 17,
        Kane = 18,
        Malyk = 19,
        Serpenti = 20,
        Shadowlord = 21,
        Shinkagezendo = 22,
        Skyfalltyrant = 23,
        Solfist = 24,
        Soulstealer = 25,
        Spelleater = 26,
        Taskmaster = 27,
        Treatdemon = 28,
        Unhallowed = 29,
        Valiant = 30,
        Vampire = 31,
        Wolfpunch = 32,
        Wraith = 33,
        Altgeneral = 34,
        Caliber = 35,
        Elyxstormblade = 36,
        Azuritelion = 37,
        Grandmasterzir = 38,
        Keshraifanblade = 39,
        Ogremonk = 40,
        Stormkage = 41,
        Anubis = 42,
        Argeon = 43,
        Reva = 44,
        Scioness = 45,
        Scioness2 = 46,
        Starstrider = 47,
        Soulreaper = 48,
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
        HighCardAtkDef = 5,
        HighCardAtk = 6,
        HighCardDef = 7,
        PokerHandsAtkDef = 8,
        PokerHandsAtk = 9,
        PokerHandsDef = 10,
        PokerHandsDollar = 11,
        PokerHandsHeal = 12,
        OnePairAtkDef = 13,
        FlushAtkDef = 14,
        StraightAtkDef = 15,
        ThreeOfAKindAtkDef = 16,
        StraightFlushAtkDef = 17,
        OddNumberAtkDef = 18,
        OddNumberAtk = 19,
        OddNumberDef = 20,
        EvenNumberAtkDef = 21,
        EvenNumberAtk = 22,
        EvenNumberDef = 23,
        NotSpecialNumbersAtkDef = 24,
        NotSpecialNumbersAtk = 25,
        NotSpecialNumbersDef = 26,
        SpecialNumbersAtkDef = 27,
        SpecialNumbersAtk = 28,
        SpecialNumbersDef = 29,
        SpecialNumbersDollar = 30,
        SpecialNumbersHeal = 31,
        PrimeNumberHeal = 32,
        PowerOfTwoHeal = 33,
        PerfectnumberHeal = 34,
        PalindromicNumberHeal = 35,
        SquareNumberHeal = 36,
        TriangularNumberHeal = 37,
        FibonacciNumberHeal = 38,
        HarshadNumberHeal = 39,
        MersenneNumberHeal = 40,
        KaprekarNumberHeal = 41,
        PellNumberHeal = 42,
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