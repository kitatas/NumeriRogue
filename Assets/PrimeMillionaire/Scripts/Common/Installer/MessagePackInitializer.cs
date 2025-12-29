#if !UNITY_EDITOR
using MessagePack;
using MessagePack.Resolvers;
using PrimeMillionaire.Common.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Common.Installer
{
    public static class MessagePackInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            StaticCompositeResolver.Instance.Register
            (
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = options;
        }
    }
}
#endif
