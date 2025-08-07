using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class CrystalView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Crystal;
        protected override int applyDamageFrame => 11;
        protected override int deadFrame => 4;
    }
}