using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Common.Utility
{
    public static class MonoBehaviourHelper
    {
        public static Coroutine LoadAsset<T>(this MonoBehaviour self, string path, Action<T> action) where T : Object
        {
            return self.StartCoroutine(ResourceHelper.LoadCor<T>(path, action));
        }
    }

    public static class ResourceHelper
    {
        public static IEnumerator LoadCor<T>(string path, Action<T> action) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                action?.Invoke(handle.Result);
            }
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