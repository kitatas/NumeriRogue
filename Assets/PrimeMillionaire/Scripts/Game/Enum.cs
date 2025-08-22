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

    public enum FinishType
    {
        None = 0,
        Clear = 1,
        Fail = 2,
    }

    public enum Side
    {
        None = 0,
        Player = 1,
        Enemy = 2,
    }

    public enum DisplayType
    {
        None = 0,
        Show = 1,
        Hide = 2,
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
        Configuration = 3,
        GiveUpConfirm = 4,
        GiveUpComplete = 5,
        Screen = 6,
        HowTo = 7,
    }

    public enum Sort
    {
        None = 0,
        Rank = 1,
        Suit = 2,
    }

    public enum BattleAnim
    {
        None = 0,
        Entry = 1,
        Exit = 2,
        Attack = 3,
        Hit = 4,
        Death = 5,
    }
}