using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class CharacterRepository
    {
        private MemoryDatabase _memoryDatabase;

        public async UniTask SetUpAsync(CancellationToken token)
        {
            var bytes = "Assets/Externals/Binary/CharacterMaster.bytes";
            var asset = await ResourceHelper.LoadAsync<TextAsset>(bytes, token);
            _memoryDatabase = new MemoryDatabase(asset.bytes);
        }

        public CharacterVO Find(CharacterType type)
        {
            if (_memoryDatabase.CharacterMasterTable.TryFindByType(type, out var master))
            {
                return master.character;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}