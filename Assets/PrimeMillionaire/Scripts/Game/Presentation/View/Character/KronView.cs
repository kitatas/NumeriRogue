using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class KronView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Kron;
        public override float applyDamageTime => 1.0f;
        public override float deadTime => 0.01f;
    }
}