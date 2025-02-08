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
        [SerializeField] private TextMeshProUGUI hp = default;
        [SerializeField] private TextMeshProUGUI atk = default;
        [SerializeField] private TextMeshProUGUI def = default;

        public async UniTask RenderAsync((CharacterVO character, ParameterVO paramter) value, CancellationToken token)
        {
            var img = await ResourceHelper.LoadAsync<Sprite>(value.character.imgPath, token);
            chara.sprite = img;
            charaName.text = value.character.name;
            hp.text = $"{value.paramter.hp}";
            atk.text = $"{value.paramter.atk}";
            def.text = $"{value.paramter.def}";
        }
    }
}