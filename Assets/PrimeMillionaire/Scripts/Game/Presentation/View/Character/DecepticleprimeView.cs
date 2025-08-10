using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DecepticleprimeView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Decepticleprime;
        protected override int applyDamageFrame => 22;
        protected override int deadFrame => 4;
    }
}