using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class HarmonyView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Harmony;
        protected override int applyDamageFrame => 8;
        protected override int deadFrame => 2;
    }
}