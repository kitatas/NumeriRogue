namespace PrimeMillionaire.Game
{
    public enum GameState
    {
        None = 0,
        Init = 1,
        SetUp = 2,
        Deal = 3,
        Order = 4,
        Battle = 5,
    }

    public enum Suit
    {
        None = 0,
        Club = 1,
        Diamond = 2,
        Heart = 3,
        Spade = 4,
    }

    public enum Side
    {
        None = 0,
        Player = 1,
        Enemy = 2,
    }

    public enum CharacterType
    {
        None = 0,
        Andromeda = 1,
        Borealjuggernaut = 2,
    }

    public enum BonusType
    {
        None = 0,
        PrimeNumber = 1,
        Suit = 2,
        Down = 3,
    }
}