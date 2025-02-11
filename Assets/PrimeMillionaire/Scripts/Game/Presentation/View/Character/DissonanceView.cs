using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DissonanceView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Dissonance;
        public override float applyDamageTime => 0.75f;
        public override float deadTime => 0.02f;
    }
}