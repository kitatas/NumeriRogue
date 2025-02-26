using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class GeneralView : CharacterView
    {
        public override CharacterType characterType => CharacterType.General;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 1;
    }
}