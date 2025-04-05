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

    public enum Suit
    {
        None = 0,
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4,
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

    public enum SkillType
    {
        None = 0,
        Odd = 1,
        OddAtk = 2,
        OddDef = 3,
        Even = 4,
        EvenAtk = 5,
        EvenDef = 6,
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