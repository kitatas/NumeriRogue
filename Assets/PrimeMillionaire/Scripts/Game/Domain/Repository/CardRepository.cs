using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class CardRepository
    {
        private MemoryDatabase _memoryDatabase;

        public async UniTask SetUpAsync(CancellationToken token)
        {
            var bytes = "Assets/Externals/Binary/CardMaster.bytes";
            var asset = await ResourceHelper.LoadAsync<TextAsset>(bytes, token);
            _memoryDatabase = new MemoryDatabase(asset.bytes);
        }

        public IEnumerable<CardVO> GetAll()
        {
            return _memoryDatabase.CardMasterTable.All
                .Select(x => x.card);
        }
    }
}