using System;
using DG.Tweening;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public abstract class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        public abstract CharacterType characterType { get; }
        public abstract float applyDamageTime { get; }
        public abstract float deadTime { get; }

        #region Animation

        private static readonly int _isAttack = Animator.StringToHash("IsAttack");
        private static readonly int _isDamage = Animator.StringToHash("IsDamage");
        private static readonly int _isDead = Animator.StringToHash("IsDead");

        public void Attack(bool value)
        {
            animator.SetBool(_isAttack, value);
        }

        public void Damage(bool value)
        {
            animator.SetBool(_isDamage, value);
        }

        public void Dead(bool value)
        {
            animator.SetBool(_isDead, value);
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