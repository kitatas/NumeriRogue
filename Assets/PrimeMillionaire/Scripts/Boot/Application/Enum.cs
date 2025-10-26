namespace PrimeMillionaire.Boot
{
    public enum BootState
    {
        None = 0,
        Login = 2,
        Interrupt = 4,
        Start = 5,
        Check = 6,
    }

    public enum ModalType
    {
        None = 0,
        Interrupt = 1,
        Update = 2,
    }
}