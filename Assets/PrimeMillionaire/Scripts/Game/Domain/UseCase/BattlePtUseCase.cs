using R3;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattlePtUseCase
    {
        private readonly ReactiveProperty<int> _playerPt;
        private readonly ReactiveProperty<int> _enemyPt;

        public BattlePtUseCase()
        {
            _playerPt = new ReactiveProperty<int>(0);
            _enemyPt = new ReactiveProperty<int>(0);
        }

        public Observable<int> playerPt => _playerPt;
        public Observable<int> enemyPt => _enemyPt;
    }
}