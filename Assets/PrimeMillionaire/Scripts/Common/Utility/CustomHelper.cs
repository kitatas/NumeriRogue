using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Common.Utility
{
    public static class DebugHelper
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }

        public static void LogException(Exception exception)
        {
#if UNITY_EDITOR
            Debug.LogException(exception);
#endif
        }
    }
    
    public static class MonoBehaviourHelper
    {
        public static Coroutine LoadAsset<T>(this MonoBehaviour self, string path, Action<T> action) where T : Object
        {
            return self.StartCoroutine(ResourceHelper.LoadCor<T>(path, action));
        }
    }

    public static class ResourceHelper
    {
        private static readonly List<AsyncOperationHandle> _handles = new();

        public static IEnumerator LoadCor<T>(string path, Action<T> action) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            yield return handle;

            try
            {
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    var msg = handle.OperationException?.Message ?? "Unknown error";
                    throw new Exception(ZString.Format("{0} load failed: {1}", path, msg));
                }

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _handles.Add(handle);
                    action?.Invoke(handle.Result);
                }
            }
            catch (Exception e)
            {
                DebugHelper.LogException(e);
                throw;
            }
        }

        public static async UniTask<T> LoadAsync<T>(string path, CancellationToken token) where T : Object
        {
            try
            {
                var handle = Addressables.LoadAssetAsync<T>(path);
                var result = await handle.WithCancellation(token);
                _handles.Add(handle);
                return result;
            }
            catch (Exception e)
            {
                DebugHelper.LogException(e);
                throw;
            }
        }

        public static void Release()
        {
            foreach (var handle in _handles.Where(handle => handle.IsValid()))
            {
                handle.Release();
            }

            _handles.Clear();
        }

        public static async UniTask LoadSceneAsync(string path, CancellationToken token)
        {
            await Addressables.LoadSceneAsync(path).ToUniTask(cancellationToken: token);
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
                Mathf.RoundToInt(Screen.height - topRight.y),
                Mathf.RoundToInt(Screen.width - topRight.x),
                Mathf.RoundToInt(bottomLeft.y)
            );
        }
    }
}