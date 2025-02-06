using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class OrderCharacterView : MonoBehaviour
    {
        [SerializeField] private Image chara = default;
        [SerializeField] private TextMeshProUGUI charaName = default;

        public async UniTask RenderAsync(CharacterVO value, CancellationToken token)
        {
            var img = await ResourceHelper.LoadAsync<Sprite>(value.imgPath, token);
            chara.sprite = img;
            charaName.text = value.name;
        }
    }
}