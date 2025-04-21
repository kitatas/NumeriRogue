using System;
using System.Collections.Generic;
using EasingCore;
using FancyScrollView;
using PrimeMillionaire.Common;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterScrollView : FancyScrollView<StageCharacterVO, ScrollContextVO>
    {
        [SerializeField] private Scroller scroller = default;
        [SerializeField] private GameObject cell = default;

        private Action<CharacterType> _order;

        protected override GameObject CellPrefab => cell;

        protected override void Initialize()
        {
            Context.onSelect = SelectCell;
            scroller.OnValueChanged(UpdatePosition);
            scroller.OnSelectionChanged(UpdateSelection);
        }

        public void Init(IList<StageCharacterVO> characters, int index, Action<CharacterType> order)
        {
            UpdateContents(characters);
            scroller.SetTotalCount(characters.Count);
            SelectCell(index);
            _order = order;
        }

        private void SelectCell(int index)
        {
            if (Context.index == index) return;
            if (ItemsSource.IsOutOfRange(index)) return;

            UpdateSelection(index);
            scroller.ScrollTo(index, 0.25f, Ease.Linear);
        }

        private void UpdateSelection(int index)
        {
            if (Context.index == index) return;

            _order?.Invoke(ItemsSource[index].character.type);
            Context.index = index;
            Refresh();
        }
    }
}