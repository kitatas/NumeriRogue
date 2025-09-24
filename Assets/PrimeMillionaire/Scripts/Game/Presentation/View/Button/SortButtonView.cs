using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View.Button
{
    public sealed class SortButtonView : BaseButtonView
    {
        [SerializeField] private Sort sort = default;
        [SerializeField] private Image backgroundImage = default;
        [SerializeField] private Sprite unpressed = default;
        [SerializeField] private Sprite pressed = default;

        private bool _isMatch;

        public Observable<Sort> pushSort => push
            .Where(_ => !_isMatch)
            .Select(_ => sort);

        public void SwitchBackground(Sort value)
        {
            _isMatch = sort == value;
            backgroundImage.sprite = _isMatch ? pressed : unpressed;
        }
    }
}