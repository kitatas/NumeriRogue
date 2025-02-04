using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private Image chara = default;

        public async UniTask RenderAsync(CharacterVO character, CancellationToken token)
        {
            var img = await ResourceHelper.LoadAsync<Sprite>(character.imgPath, token);
            chara.sprite = img;
        }
    }
}