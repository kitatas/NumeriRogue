using System;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public abstract class CharacterView : MonoBehaviour
    {
        public abstract CharacterType characterType { get; }
        public abstract float applyDamageTime { get; }

        #region Animation

        private static readonly int _isAttack = Animator.StringToHash("IsAttack");
        private static readonly int _isDamage = Animator.StringToHash("IsDamage");
        private static readonly int _isDead = Animator.StringToHash("IsDead");

        private Animator _animator;
        private Animator animator => _animator ??= GetComponent<Animator>();

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

        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer spriteRenderer => _spriteRenderer ??= GetComponent<SpriteRenderer>();

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
    }
}