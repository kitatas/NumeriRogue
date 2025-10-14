namespace PrimeMillionaire.Common
{
    public sealed class UrlConfig
    {
        // TODO: fix url
        public const string URL_BASE = "https://kitatas.github.io/games/route_invent_puzzle/";
        public const string URL_LICENSE = URL_BASE + "license";
        public const string URL_CREDIT = URL_BASE + "credit";
        public const string URL_POLICY = URL_BASE + "policy";

        public const string DEVELOPER_APP_URL = "https://play.google.com/store/apps/developer?id=KitaLab";
    }

    public sealed class SaveConfig
    {
        public const string ES3_KEY = "";
    }

    public sealed class ExceptionConfig
    {
        public const string REBOOT_MESSAGE = "Return to title.";
        public const string RETRY_MESSAGE = "Retry.";
        public const string QUIT_MESSAGE = "Exit this app.";

        public const string UNKNOWN_ERROR = "UNKNOWN_ERROR";
        public const string NOT_FOUND_BGM = "NOT_FOUND_BGM";
        public const string NOT_FOUND_BATTLE_ANIMATION = "NOT_FOUND_BATTLE_ANIMATION";
        public const string NOT_FOUND_BONUS = "NOT_FOUND_BONUS";
        public const string NOT_FOUND_BUTTON = "NOT_FOUND_BUTTON";
        public const string NOT_FOUND_CARD = "NOT_FOUND_CARD";
        public const string NOT_FOUND_CHARACTER = "NOT_FOUND_CHARACTER";
        public const string NOT_FOUND_DISPLAY =  "NOT_FOUND_DISPLAY";
        public const string NOT_FOUND_FADE = "NOT_FOUND_FADE";
        public const string NOT_FOUND_FINISH = "NOT_FOUND_FINISH";
        public const string NOT_FOUND_FX = "NOT_FOUND_FX";
        public const string NOT_FOUND_LOAD = "NOT_FOUND_LOAD";
        public const string NOT_FOUND_MODAL = "NOT_FOUND_MODAL";
        public const string NOT_FOUND_POKER_HANDS = "NOT_FOUND_POKER_HANDS";
        public const string NOT_FOUND_SCENE = "NOT_FOUND_SCENE";
        public const string NOT_FOUND_SE = "NOT_FOUND_SE";
        public const string NOT_FOUND_SIDE = "NOT_FOUND_SIDE";
        public const string NOT_FOUND_SKILL = "NOT_FOUND_SKILL";
        public const string NOT_FOUND_SORT = "NOT_FOUND_SORT";
        public const string NOT_FOUND_SOUND = "NOT_FOUND_SOUND";
        public const string NOT_FOUND_STAGE = "NOT_FOUND_STAGE";
        public const string NOT_FOUND_STATE = "NOT_FOUND_STATE";
        public const string NOT_FOUND_SUIT = "NOT_FOUND_SUIT";
        public const string NOT_FOUND_WEBVIEW = "NOT_FOUND_WEBVIEW";
        public const string FAILED_DEPENDENCY_RESOLUTION = "FAILED_DEPENDENCY_RESOLUTION";
        public const string FAILED_LOAD_INTERRUPT = "FAILED_LOAD_INTERRUPT";
        public const string FAILED_LOAD_PROGRESS = "FAILED_LOAD_PROGRESS";
        public const string FAILED_LOGIN = "FAILED_LOGIN";
        public const string INVALID_SOUND_DURATION = "INVALID_SOUND_DURATION";
        public const string OUT_OF_RANGE = "OUT_OF_RANGE";
        public const string MAX_RETRY_COUNT = "MAX_RETRY_COUNT";
    }

    public sealed class UiConfig
    {
        public const float POPUP_DURATION = 0.25f;
        public const float FLASH_DURATION = 1.0f;
        public const float TWEEN_DURATION = 0.25f;
        public const float BUTTON_DURATION = 0.1f;
    }
}