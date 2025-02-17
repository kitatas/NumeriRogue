using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Common.Utility
{
    public static class ResourceHelper
    {
        public static T Load<T>(string path) where T : Object
        {
            return Addressables.LoadAssetAsync<T>(path).Result;
        }
        
        public static async UniTask<T> LoadAsync<T>(string path, CancellationToken token) where T : Object
        {
            return await Addressables.LoadAssetAsync<T>(path).WithCancellation(token);
        }

        public static async UniTask<T> LoadExternalsAsync<T>(string path, CancellationToken token) where T : Object
        {
            return await LoadAsync<T>($"Assets/Externals/{path}", token);
        }
    }

    public static class UniTaskHelper
    {
        public static UniTask DelayAsync(float duration, CancellationToken token)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
        }
    }
}