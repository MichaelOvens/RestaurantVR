using Interaction.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSurvey
{
    public class MenuSelectionRow : MonoBehaviour
    {
        public EventHandler<MenuItemData> OnSelectionToggled;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Image _checkmark;
        [SerializeField] private UIToggle _toggle;

        private MenuItemData _itemData;

        private void Awake()
        {
            _toggle.OnValueChanged += OnToggleValueChanged;
        }

        public void Populate(MenuItemData itemData)
        {
            _itemData = itemData;
            _textField.text = itemData.Text;
        }

        public void UpdateLayout()
        {
            _rectTransform.sizeDelta = new Vector2()
            {
                x = _rectTransform.sizeDelta.x,
                y = _textField.preferredHeight
            };
        }

        private void OnToggleValueChanged(object sender, bool isSelected)
        {
            _itemData.IsSelected = isSelected;
            _checkmark.enabled = isSelected;
            OnSelectionToggled?.Invoke(this, _itemData);
        }
    }
}
