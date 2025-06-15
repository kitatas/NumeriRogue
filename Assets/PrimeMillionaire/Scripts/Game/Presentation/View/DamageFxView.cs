using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DamageFxView : MonoBehaviour
    {
        [SerializeField] private Animator animator = default;

        private static readonly int _isPlay = Animator.StringToHash("IsPlay");

        public void Play()
        {
            SetIsPlay(true);

            this.Delay(1.5f, () => SetIsPlay(false));
        }

        private void SetIsPlay(bool value) => animator.SetBool(_isPlay, value);
    }
}