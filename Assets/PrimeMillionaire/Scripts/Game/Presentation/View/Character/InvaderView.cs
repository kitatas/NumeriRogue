using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class InvaderView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Invader;
        protected override int applyDamageFrame => 24;
        protected override int deadFrame => 4;
    }
}