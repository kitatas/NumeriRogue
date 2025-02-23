using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DissonanceView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Dissonance;
        protected override int attackFinishFrame => 20;
        protected override int applyDamageFrame => 8;
        protected override int deadFrame => 2;
    }
}