using System;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class TurnUseCase : IDisposable
    {
        private readonly TurnEntity _turnEntity;
        private readonly ReactiveProperty<int> _turn;

        public TurnUseCase(TurnEntity turnEntity)
        {
            _turnEntity = turnEntity;
            _turn = new ReactiveProperty<int>(_turnEntity.currentValue);
        }

        public Observable<int> turn => _turn.Where(x => x != 0);

        public void Increment()
        {
            _turnEntity.Add(1);
            _turn.Value = _turnEntity.currentValue;
        }

        public void Reset()
        {
            _turnEntity.Reset();
            _turn.Value = _turnEntity.currentValue;
        }

        public void Dispose()
        {
            _turn?.Dispose();
        }
    }
}