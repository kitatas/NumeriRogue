using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View.Button
{
    public sealed class TopModalButtonView : BaseModalButtonView
    {
        [SerializeField] private ModalType modalType = default;
        [SerializeField] private bool isActivateModal = default;

        public ModalType type => modalType;
        public bool isActivate => isActivateModal;
    }
}