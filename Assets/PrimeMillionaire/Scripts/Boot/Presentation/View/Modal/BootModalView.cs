using PrimeMillionaire.Common.Presentation.View.Modal;

namespace PrimeMillionaire.Boot.Presentation.View.Modal
{
    public abstract class BootModalView : BaseModalView
    {
        public abstract ModalType type { get; }
    }
}