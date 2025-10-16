using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly PlayFabRepository _playFabRepository;
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(PlayFabRepository playFabRepository, SaveRepository saveRepository)
        {
            _playFabRepository = playFabRepository;
            _saveRepository = saveRepository;
        }

        public async UniTask<bool> LoginAsync(CancellationToken token)
        {
            var saveData = _saveRepository.Load();
            if (saveData.IsEmptyUid())
            {
                var user = await CreateAsync(token);
                saveData.uid = user.uid;
                _saveRepository.Save(saveData);
            }
            else
            {
                var user = await _playFabRepository.LoginAsync(saveData.uid, token);
            }

            return true;
        }

        private async UniTask<UserVO> CreateAsync(CancellationToken token)
        {
            while (true)
            {
                var uid = Ulid.NewUlid().ToString();
                var user = await _playFabRepository.LoginAsync(uid, token);

                // 新規ユーザーでなければ uid を再生成
                if (user.isNewly) return user;
            }
        }
    }
}