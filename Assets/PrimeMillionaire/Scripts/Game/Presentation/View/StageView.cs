using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class StageView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background = default;
        [SerializeField] private SpriteRenderer midground = default;
        [SerializeField] private SpriteRenderer midgroundGlow = default;
        [SerializeField] private TextMeshProUGUI maxEnemyCount = default;

        public async UniTask LoadAsync(StageVO stage, CancellationToken token)
        {
            var resourceLoads = new List<UniTask<Sprite>>
            {
                ResourceHelper.LoadAsync<Sprite>(stage.bgPath, token),
                ResourceHelper.LoadAsync<Sprite>(stage.mgPath, token)
            };
            if (stage.hasGlow) resourceLoads.Add(ResourceHelper.LoadAsync<Sprite>(stage.glowPath, token));

            var results = await UniTask.WhenAll(resourceLoads);
            background.sprite = results[0];
            midground.sprite = results[1];
            if (stage.hasGlow) midgroundGlow.sprite = results[2];

            maxEnemyCount.text = $"{stage.maxEnemyCount}";
        }
    }
}