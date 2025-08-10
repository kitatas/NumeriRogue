using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class GrymView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Grym;
        protected override int applyDamageFrame => 10;
        protected override int deadFrame => 4;
    }
}