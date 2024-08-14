using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Interaction.UI;

namespace Survey.Likert
{
    public class LikertResponseToggle : MonoBehaviour
    {
        public EventHandler<int> OnToggleSelected;
        public EventHandler<int> OnToggleDeselected;

        [SerializeField] private UIToggle _toggle;

        private int _responseId;

        public void Populate(UIToggleGroup toggleGroup, int responseId)
        {
            _responseId = responseId;
            _toggle.ToggleGroupMember = toggleGroup;
            _toggle.OnValueChanged += OnToggleValueChanged;
        }

        private void OnToggleValueChanged(object sender, bool isSelected)
        {
            if (isSelected) OnToggleSelected?.Invoke(this, _responseId);
            else OnToggleDeselected?.Invoke(this, _responseId);
        }
    }
}