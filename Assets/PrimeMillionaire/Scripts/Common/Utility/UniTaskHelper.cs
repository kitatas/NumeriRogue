using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Common.Utility
{
    public static class UniTaskHelper
    {
        public static UniTask DelayAsync(float duration, CancellationToken token)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        }
    }
}