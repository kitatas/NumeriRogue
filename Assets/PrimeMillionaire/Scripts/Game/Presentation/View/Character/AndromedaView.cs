using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class AndromedaView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Andromeda;
        protected override int applyDamageFrame => 14;
        protected override int deadFrame => 3;
    }
}