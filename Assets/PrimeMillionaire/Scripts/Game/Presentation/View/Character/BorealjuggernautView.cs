using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BorealjuggernautView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Borealjuggernaut;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 6;
    }
}