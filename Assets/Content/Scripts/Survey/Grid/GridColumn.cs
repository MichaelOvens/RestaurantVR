using System;
using TMPro;
using UnityEngine;

using Interaction.UI;

namespace Survey.Grid
{
    public class GridColumn : MonoBehaviour
    {
        public float PreferredHeight => _textElement.preferredHeight;

        [SerializeField] private GridResponseData _responseData;
        [SerializeField] private UIToggle _toggle;
        [SerializeField] private TextMeshProUGUI _textElement;

        public void UpdateLayout()
        {
            // Do nothing
        }

        public void Populate(GridResponseData response, UIToggleGroup toggleGroup)
        {
            if (response == null || string.IsNullOrEmpty(response.id) || string.IsNullOrEmpty(response.text))
            {
                Debug.LogError($"WARNING: Response is missing data ({response?.id},{response?.text})");
            }

            _responseData = response;
            _textElement.text = response.text;

            switch (response.type)
            {
                case GridResponseType.GroupMember:
                    _toggle.ToggleGroupMember = toggleGroup;
                    _toggle.ToggleGroupExclusive = null;
                    break;
                case GridResponseType.GroupExclusive:
                    _toggle.ToggleGroupMember = null;
                    _toggle.ToggleGroupExclusive = toggleGroup;
                    break;
            }

            _toggle.OnValueChanged += OnToggleValueChanged;
        }

        private void OnToggleValueChanged(object sender, bool isSelected)
        {
            _responseData.selected = isSelected;
        }
    }
}