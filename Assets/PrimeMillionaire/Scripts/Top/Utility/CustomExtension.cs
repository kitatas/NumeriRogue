using System;
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
                _ => throw new Exception()
            };
        }
    }
}