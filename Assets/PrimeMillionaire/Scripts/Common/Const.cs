namespace PrimeMillionaire.Common
{
    public sealed class VersionConfig
    {
        public const int MAJOR_VERSION = 0;
        public const int MINOR_VERSION = 14;
        public static readonly string APP_VERSION = $"{MAJOR_VERSION.ToString()}.{MINOR_VERSION.ToString()}";
    }

    public sealed class SaveConfig
    {
        public const string ES3_KEY = "";
    }

    public sealed class UiConfig
    {
        public const float POPUP_DURATION = 0.25f;
        public const float FLASH_DURATION = 1.0f;
        public const float TWEEN_DURATION = 0.25f;
    }
}