using PrimeMillionaire.Common.Presentation.View.Modal;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View.Modal
{
    public sealed class BootModalView : BaseModalView
    {
        [SerializeField] private ModalType modalType = default;

        public ModalType type => modalType;
    }
}