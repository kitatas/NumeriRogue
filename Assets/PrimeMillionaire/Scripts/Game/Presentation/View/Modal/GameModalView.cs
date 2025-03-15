using PrimeMillionaire.Common.Presentation.View.Modal;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View.Modal
{
    public class GameModalView : BaseModalView
    {
        [SerializeField] private ModalType modalType = default;

        public ModalType type => modalType;
    }
}