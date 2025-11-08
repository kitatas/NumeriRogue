using System.Collections.Generic;

namespace PrimeMillionaire.Boot
{
    public sealed class BootConfig
    {
        public const BootState INIT_STATE = BootState.Load;
    }

    public sealed class SplashConfig
    {
        public const float DURATION = 0.5f;

        public static readonly List<SplashType> TYPES = new()
        {
            SplashType.Developer,
            SplashType.PlayFab,
        };
    }
}