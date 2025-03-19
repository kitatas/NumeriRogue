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

    public sealed class ExceptionConfig
    {
        public const string NOT_FOUND_BUTTON = "NOT_FOUND_BUTTON";
        public const string NOT_FOUND_CARD = "NOT_FOUND_CARD";
        public const string NOT_FOUND_CHARACTER = "NOT_FOUND_CHARACTER";
        public const string NOT_FOUND_LOAD = "NOT_FOUND_LOAD";
        public const string NOT_FOUND_MODAL = "NOT_FOUND_MODAL";
        public const string NOT_FOUND_SCENE = "NOT_FOUND_SCENE";
        public const string NOT_FOUND_SKILL = "NOT_FOUND_SKILL";
        public const string NOT_FOUND_SKILL_DESCRIPTION = "NOT_FOUND_SKILL_DESCRIPTION";
        public const string NOT_FOUND_SKILL_PRICE = "NOT_FOUND_SKILL_PRICE";
        public const string NOT_FOUND_STAGE = "NOT_FOUND_STAGE";
        public const string NOT_FOUND_STATE = "NOT_FOUND_STATE";
        public const string NOT_FOUND_SUIT = "NOT_FOUND_SUIT";
        public const string FAILED_LOAD_PROGRESS = "FAILED_LOAD_PROGRESS";
        public const string FAILED_LOGIN = "FAILED_LOGIN";
    }

    public sealed class UiConfig
    {
        public const float POPUP_DURATION = 0.25f;
        public const float FLASH_DURATION = 1.0f;
        public const float TWEEN_DURATION = 0.25f;
    }
}