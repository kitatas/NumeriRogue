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

        public async UniTask RenderAsync(CharacterVO character, CancellationToken token)
        {
            var view = Instantiate(characterView, viewport);
            await view.RenderAsync(character, token);
        }
    }
}