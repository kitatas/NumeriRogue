using System;
using System.Collections.Generic;
using EasingCore;
using FancyScrollView;
using PrimeMillionaire.Common;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class CharacterScrollView : FancyScrollView<CharacterVO, ScrollContextVO>
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

        public void Init(IList<CharacterVO> characters, Action<CharacterType> order)
        {
            UpdateContents(characters);
            scroller.SetTotalCount(characters.Count);
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

            _order?.Invoke(ItemsSource[index].type);
            Context.index = index;
            Refresh();
        }
    }
}