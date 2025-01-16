using FastEnumUtility;

namespace PrimeMillionaire.Common
{
    public sealed class LoadVO
    {
        public readonly SceneName sceneName;
        public readonly LoadType loadType;

        public LoadVO(SceneName sceneName, LoadType loadType)
        {
            this.sceneName = sceneName;
            this.loadType = loadType;
        }

        public string name => sceneName.FastToString();
        public bool isFade => loadType == LoadType.Fade;
    }
}