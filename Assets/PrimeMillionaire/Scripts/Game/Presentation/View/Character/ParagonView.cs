using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class ParagonView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Paragon;
        protected override int attackFinishFrame => 27;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 3;
    }
}