namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BorealjuggernautView : CharacterView
    {
        public override CharacterType characterType => CharacterType.Borealjuggernaut;
        public override float applyDamageTime => 1.03f;
        public override float deadTime => 0.06f;
    }
}