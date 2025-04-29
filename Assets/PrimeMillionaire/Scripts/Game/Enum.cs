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
        Pick = 6,
        Load = 7,
        Fail = 8,
        Clear = 9,
        Restart = 10,
    }

    public enum Side
    {
        None = 0,
        Player = 1,
        Enemy = 2,
    }

    public enum Fade
    {
        None = 0,
        In = 1,
        Out = 2,
    }

    public enum ModalType
    {
        None = 0,
        PickSkill = 1,
        Menu = 2,
    }
}