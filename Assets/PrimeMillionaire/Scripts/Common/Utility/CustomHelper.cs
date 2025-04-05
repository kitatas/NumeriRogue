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

        public static UniTask DelayFrameAsync(int frame, CancellationToken token)
        {
            return UniTask.DelayFrame(frame, cancellationToken: token);
        }
    }

    public static class RectTransformHelper
    {
        public static (int, int, int, int) GetMargins(this RectTransform self, Canvas canvas)
        {
            var corners = new Vector3[4];
            self.GetWorldCorners(corners);

            Vector3 bottomLeft = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[0]);
            Vector3 topRight = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[2]);

            return (
                Mathf.RoundToInt(bottomLeft.x),
                Mathf.RoundToInt(Screen.height - topRight.y) - 10,
                Mathf.RoundToInt(Screen.width - topRight.x),
                Mathf.RoundToInt(bottomLeft.y) + 10
            );
        }
    }
}