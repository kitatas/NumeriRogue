using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class AndromedaView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Andromeda;
        public override float applyDamageTime => 1.02f;
        public override float deadTime => 0.03f;
    }
}