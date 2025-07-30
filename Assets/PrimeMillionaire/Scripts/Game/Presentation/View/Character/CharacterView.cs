using System;
using DG.Tweening;
using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public abstract class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        public abstract CharacterType characterType { get; }
        protected abstract int applyDamageFrame { get; }
        protected abstract int deadFrame { get; }

        public float applyDamageTime => applyDamageFrame / 12.0f; 
        public float deadTime => deadFrame / 12.0f;

        #region Animation

        private static readonly int _isAttack = Animator.StringToHash("IsAttack");
        private static readonly int _isHit = Animator.StringToHash("IsHit");
        private static readonly int _isDeath = Animator.StringToHash("IsDeath");

        public void Attack(bool value)
        {
            animator.SetBool(_isAttack, value);
        }

        public void Hit(bool value)
        {
            animator.SetBool(_isHit, value);
        }

        public void Death(bool value)
        {
            animator.SetBool(_isDeath, value);
        }

        #endregion

        #region Sprite

        public void FlipX(Side side)
        {
            spriteRenderer.flipX = side switch
            {
                Side.Player => true,
                Side.Enemy => false,
                _ => throw new Exception(),
            };
        }

        #endregion

        #region Tween

        public Tween TweenPositionX(float x, float duration)
        {
            return transform
                .DOLocalMoveX(x, duration)
                .SetLink(gameObject);
        }

        #endregion
    }
}