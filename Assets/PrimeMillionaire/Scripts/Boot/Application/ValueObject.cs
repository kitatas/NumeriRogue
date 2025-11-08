using PrimeMillionaire.Common;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Boot
{
    public sealed class ModalVO : BaseModalVO<ModalType>
    {
        public ModalVO(ModalType type, bool isActivate) : base(type, isActivate)
        {
        }
    }

    public sealed class SplashVO : ICommand
    {
        public readonly Sprite sprite;
        public readonly float duration;
        public readonly bool isActivate;

        public SplashVO(Sprite sprite, float duration, bool isActivate)
        {
            this.sprite = sprite;
            this.duration = duration;
            this.isActivate = isActivate;
        }
    }
}