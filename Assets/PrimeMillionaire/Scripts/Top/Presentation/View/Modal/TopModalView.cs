using PrimeMillionaire.Common.Presentation.View.Modal;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View.Modal
{
    public class TopModalView : BaseModalView
    {
        [SerializeField] private ModalType modalType = default;

        public ModalType type => modalType;
    }
}