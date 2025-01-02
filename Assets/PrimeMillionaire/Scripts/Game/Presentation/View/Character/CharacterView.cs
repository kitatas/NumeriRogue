using System;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public abstract class CharacterView : MonoBehaviour
    {
        public abstract CharacterType characterType { get; }

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