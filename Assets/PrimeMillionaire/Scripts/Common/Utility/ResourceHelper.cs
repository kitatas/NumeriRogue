using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace PrimeMillionaire.Common.Utility
{
    public static class ResourceHelper
    {
        public static async UniTask<T> LoadAsync<T>(string path, CancellationToken token) where T : UnityEngine.Object
        {
            return await Addressables.LoadAssetAsync<T>(path).WithCancellation(token);
        }
    }
}