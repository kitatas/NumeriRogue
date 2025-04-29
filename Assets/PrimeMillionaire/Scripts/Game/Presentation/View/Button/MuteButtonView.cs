using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View.Button
{
    public sealed class MuteButtonView : BaseButtonView
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private Sprite on = default;
        [SerializeField] private Sprite off = default;

        public bool isMute { get; private set; }

        public void Init(VolumeVO volume) => Set(volume.isMute);
        public void Switch() => Set(!isMute);

        public void Set(bool value)
        {
            isMute = value;
            icon.sprite = isMute ? on : off;
        }
    }
}