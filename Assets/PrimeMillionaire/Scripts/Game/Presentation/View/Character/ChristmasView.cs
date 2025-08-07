using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class ChristmasView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Christmas;
        protected override int applyDamageFrame => 16;
        protected override int deadFrame => 4;
    }
}