using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View.Button
{
    public sealed class GameModalButtonView : BaseModalButtonView
    {
        [SerializeField] private ModalType modalType = default;
        [SerializeField] private bool isActivateModal = default;

        public ModalType type => modalType;
        public bool isActivate => isActivateModal;
    }
}