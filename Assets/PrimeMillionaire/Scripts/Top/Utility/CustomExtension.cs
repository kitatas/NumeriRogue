using PrimeMillionaire.Common;

namespace PrimeMillionaire.Top.Utility
{
    public static class CustomExtension
    {
        public static string ToURL(this ModalType self)
        {
            return self switch
            {
                ModalType.License => UrlConfig.URL_LICENSE,
                ModalType.Credit => UrlConfig.URL_CREDIT,
                ModalType.Policy => UrlConfig.URL_POLICY,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_WEBVIEW),
            };
        }
    }
}