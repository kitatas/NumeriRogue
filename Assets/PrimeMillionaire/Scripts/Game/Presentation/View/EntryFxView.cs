using System;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class EntryFxView : MonoBehaviour
    {
        [SerializeField] private Animator animator = default;

        private static readonly int _isEntry = Animator.StringToHash("IsEntry");
        private static readonly int _isExit = Animator.StringToHash("IsExit");

        public void Entry(Action action)
        {
            SetIsEntry(true);
            SetIsExit(false);

            this.Delay(8.0f / 12.0f, () => action?.Invoke());
        }

        public void Exit(Action action)
        {
            SetIsExit(true);

            this.Delay(11.0f / 12.0f, () => action?.Invoke());
        }

        private void SetIsEntry(bool value) => animator.SetBool(_isEntry, value);
        private void SetIsExit(bool value) => animator.SetBool(_isExit, value);
    }
}