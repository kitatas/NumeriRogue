using PrimeMillionaire.Common;

namespace PrimeMillionaire.Boot
{
    public sealed class ModalVO : BaseModalVO<ModalType>
    {
        public ModalVO(ModalType type, bool isActivate) : base(type, isActivate)
        {
        }
    }
}