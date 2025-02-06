using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterListView : MonoBehaviour
    {
        [SerializeField] private CharacterView characterView = default;
        [SerializeField] private Transform viewport = default;
        [SerializeField] private OrderCharacterView orderCharacterView = default;

        public async UniTask<CharacterView> RenderAsync(CharacterVO character, CancellationToken token)
        {
            var view = Instantiate(characterView, viewport);
            await view.RenderAsync(character, token);
            return view;
        }

        public async UniTask OrderAsync(CharacterVO value, CancellationToken token)
        {
            await orderCharacterView.RenderAsync(value, token);
        }
    }
}