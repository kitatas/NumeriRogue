using DG.Tweening;
using PrimeMillionaire.Common.Presentation.View.Modal;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View.Modal
{
    public sealed class TopModalView : BaseModalView
    {
        [SerializeField] private ModalType modalType = default;
        [SerializeField] private ScrollRect scrollRect = default;

        public ModalType type => modalType;

        public override Tween Hide(float duration)
        {
            return base.Hide(duration)
                .OnComplete(() => scrollRect.verticalNormalizedPosition = 1.0f);
        }
    }
}