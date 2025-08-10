using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class EmpView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Emp;
        protected override int applyDamageFrame => 12;
        protected override int deadFrame => 4;
    }
}