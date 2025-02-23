using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class KronView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Kron;
        protected override int attackFinishFrame => 23;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 2;
    }
}