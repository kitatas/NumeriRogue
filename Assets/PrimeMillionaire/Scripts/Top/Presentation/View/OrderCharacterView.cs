using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private DeckView deckView = default;

        public async UniTask RenderAsync(OrderCharacterVO value, CancellationToken token)
        {
            var img = await ResourceHelper.LoadAsync<Sprite>(value.character.imgPath, token);
            chara.sprite = img;
            charaName.text = value.character.name;
            hp.text = ZString.Format("{0}", value.character.parameter.hp);
            atk.text = ZString.Format("{0}", value.character.parameter.atk);
            def.text = ZString.Format("{0}", value.character.parameter.def);
            deckView.Render(value.deck);
        }
    }
}