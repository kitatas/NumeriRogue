using System;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class SceneUseCase : IDisposable
    {
        private readonly Subject<LoadVO> _load;

        public SceneUseCase()
        {
            _load = new Subject<LoadVO>();
        }

        public Observable<LoadVO> load => _load.Where(x => x.sceneName != SceneName.None);

        public void Load(SceneName sceneName, LoadType type)
        {
            _load?.OnNext(new LoadVO(sceneName, type));
        }

        public void Dispose()
        {
            _load?.Dispose();
        }
    }
}