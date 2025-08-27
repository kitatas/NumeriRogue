using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class InterruptUseCase : IDisposable
    {
        private readonly SaveRepository _saveRepository;
        private readonly Subject<ButtonType> _pressButton;

        public InterruptUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
            _pressButton = new Subject<ButtonType>();
        }

        public bool HasInterrupt()
        {
            return _saveRepository.TryLoadInterrupt(out _);
        }

        public void PressButton(ButtonType type)
        {
            _pressButton?.OnNext(type);
        }

        public async UniTask<ButtonType> PressButtonAsync(CancellationToken token)
        {
            var button = await _pressButton.FirstAsync(cancellationToken: token);

            // 復帰しない場合は中断データを削除
            if (button == ButtonType.Cancel) _saveRepository.DeleteInterrupt();

            return button;
        }

        public void Dispose()
        {
            _pressButton?.Dispose();
        }
    }
}