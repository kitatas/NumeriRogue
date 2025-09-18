namespace PrimeMillionaire.Boot
{
    public enum BootState
    {
        None = 0,
        Load = 1,
        Login = 2,
        Restart = 3,
        Interrupt = 4,
    }

    public enum ModalType
    {
        None = 0,
        Interrupt = 1,
    }
}