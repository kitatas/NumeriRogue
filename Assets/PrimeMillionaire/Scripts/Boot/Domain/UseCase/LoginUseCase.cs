using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class LoginUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;
        private readonly SaveRepository _saveRepository;

        public LoginUseCase(UserEntity userEntity, PlayFabRepository playFabRepository, SaveRepository saveRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
            _saveRepository = saveRepository;
        }

        public async UniTask<bool> LoginAsync(CancellationToken token)
        {
            var user = await GetLoginUserAsync(token);
            _userEntity.Set(user);

            return true;
        }

        private async UniTask<UserVO> GetLoginUserAsync(CancellationToken token)
        {
            var saveData = _saveRepository.Load();
            if (saveData.isEmptyUid)
            {
                var user = await CreateAsync(token);
                _saveRepository.Save(user);
                return user;
            }
            else
            {
                return await _playFabRepository.LoginAsync(saveData.uid, token);
            }
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