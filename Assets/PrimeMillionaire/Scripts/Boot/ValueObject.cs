using VitalRouter;

namespace PrimeMillionaire.Boot
{
    public sealed class ModalVO : ICommand
    {
        public readonly ModalType type;

        public ModalVO(ModalType type)
        {
            this.type = type;
        }
    }
}