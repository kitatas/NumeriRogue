using UnityEngine;

namespace PrimeMillionaire.Boot.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SplashData), menuName = "DataTable/" + nameof(SplashData))]
    public sealed class SplashData : ScriptableObject
    {
        [SerializeField] private SplashType splashType = default;
        [SerializeField] private Sprite splashSprite = default;

        public SplashType type => splashType;
        public Sprite sprite => splashSprite;
    }
}