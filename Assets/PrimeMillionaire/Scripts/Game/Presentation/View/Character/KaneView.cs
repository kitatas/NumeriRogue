using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class KaneView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Kane;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 4;
    }
}