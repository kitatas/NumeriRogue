using FancyScrollView;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterCellView : FancyCell<CharacterVO, ScrollContextVO>
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private Image body = default;
        [SerializeField] private Image chara = default;
        [SerializeField] private TextMeshProUGUI characterName = default;

        public CharacterVO character { get; private set; }
        private float _currentPosition;

        private static readonly int _scroll = Animator.StringToHash("Scroll");

        private void Start()
        {
            body.OnPointerClickAsObservable()
                .Subscribe(_ => Context.onSelect?.Invoke(Index))
                .AddTo(this);
        }

        public override void UpdateContent(CharacterVO value)
        {
            character = value;
            chara.sprite = ResourceHelper.Load<Sprite>(character.imgPath);
            characterName.text = character.name;
        }

        public override void UpdatePosition(float position)
        {
            _currentPosition = position;

            if (animator.isActiveAndEnabled)
            {
                animator.Play(_scroll, -1, position);
            }

            animator.speed = 0;
        }

        private void OnEnable()
        {
            UpdatePosition(_currentPosition);
        }
    }
}