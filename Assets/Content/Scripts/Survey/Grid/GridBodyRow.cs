using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Interaction.UI;

namespace Survey.Grid
{
    public class GridBodyRow : MonoBehaviour
    {
        public ReadOnlyCollection<GridColumn> Columns => _columns.AsReadOnly();

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private List<GridColumn> _columns;

        public void Populate(List<GridResponseData> responses, UIToggleGroup toggleGroup)
        {
            if (responses.Count() > _columns.Count())
            {
                Debug.LogError($"Too many responses {responses.Count()} " +
                    $"for the number of columns ({_columns.Count()})");
            }

            for (int i = 0; i < responses.Count; i++)
            {
                _columns[i].Populate(responses[i], toggleGroup);
            }

            // Disable any unused columns
            for (int i = responses.Count; i < _columns.Count; i++)
            {
                _columns[i].gameObject.SetActive(false);
            }

            UpdateLayout();
        }

        public void UpdateLayout()
        {
            float minHeight = _columns.Where(column => column.isActiveAndEnabled).Max(column => column.PreferredHeight);
            _rectTransform.sizeDelta = new Vector2()
            {
                x = _rectTransform.sizeDelta.x,
                y = minHeight
            };

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }
}