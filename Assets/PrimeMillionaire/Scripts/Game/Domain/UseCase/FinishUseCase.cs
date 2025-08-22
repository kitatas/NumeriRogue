using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class FinishUseCase
    {
        public async UniTask FadeInViewAsync(FinishType type, CancellationToken token)
        {
            await Router.Default.PublishAsync(new FinishVO(type), token);

            await UniTaskHelper.DelayAsync(1.0f, token);
        }
    }
}