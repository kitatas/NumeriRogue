using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class CinderaView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Cindera;
        protected override int applyDamageFrame => 32;
        protected override int deadFrame => 4;
    }
}