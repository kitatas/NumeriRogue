using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class ChaosknightView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Chaosknight;
        protected override int attackFinishFrame => 35;
        protected override int applyDamageFrame => 26;
        protected override int deadFrame => 1;
    }
}