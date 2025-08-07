using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class AntiswarmView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Antiswarm;
        protected override int applyDamageFrame => 34;
        protected override int deadFrame => 4;
    }
}