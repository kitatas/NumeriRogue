using FancyScrollView;
using FastEnumUtility;
using PrimeMillionaire.Common.Utility;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterCellView : FancyCell<StageCharacterVO, ScrollContextVO>
    {
        [SerializeField] private Animator animator = default;
        [SerializeField] private Image body = default;
        [SerializeField] private Image chara = default;
        [SerializeField] private GameObject newLabel = default;
        [SerializeField] private GameObject clearLabel = default;
        [SerializeField] private TextMeshProUGUI characterId = default;
        [SerializeField] private TextMeshProUGUI characterName = default;

        private float _currentPosition;

        private static readonly int _scroll = Animator.StringToHash("Scroll");

        private void Start()
        {
            body.OnPointerClickAsObservable()
                .Subscribe(_ => Context.onSelect?.Invoke(Index))
                .AddTo(this);
        }

        public override void UpdateContent(StageCharacterVO value)
        {
            this.LoadAsset<Sprite>(value.character.imgPath, x => chara.sprite = x);
            characterId.text = $"No.{value.character.type.ToInt32():000}";
            characterName.text = value.character.name;

            newLabel.SetActive(value.progress.isNew);
            clearLabel.SetActive(value.progress.isClear);
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