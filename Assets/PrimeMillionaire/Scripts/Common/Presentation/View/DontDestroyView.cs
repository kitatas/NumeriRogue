using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View
{
    public sealed class DontDestroyView : MonoBehaviour
    {
        private static DontDestroyView _instance;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}