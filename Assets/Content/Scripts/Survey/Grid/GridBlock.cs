using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Interaction.UI;

namespace Survey.Grid
{
    public class GridBlock : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridHeaderRow _header;
        [SerializeField] private List<GridBodyRow> _bodyRows;
        [SerializeField] private UIToggleGroup _toggleGroup;

        public void Populate(GridBlockData blockData)
        {
            // Set all the response selections to false
            foreach (var response in blockData.Responses)
            {
                response.selected = false;
            }

            // Populate the survey form
            _header.Populate(blockData);

            switch (blockData.Type)
            {
                case GridBlockType.SingleResponse:
                    _toggleGroup.AllowMultipleMembersToBeSwitchedOn = false;
                    break;
                case GridBlockType.MultiResponse:
                    _toggleGroup.AllowMultipleMembersToBeSwitchedOn = true;
                    break;
            }

            int numberOfColumns = blockData.Responses.Count;
            int columnsPerRow = blockData.PrefabRow.Columns.Count;
            int numberOfRows = (numberOfColumns + columnsPerRow - 1) / columnsPerRow;

            int startIndex = 0;
            int endIndex = 3;

            for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
            {
                startIndex = rowIndex * columnsPerRow;
                endIndex = Mathf.Min(startIndex + 3, numberOfColumns);

                GridBodyRow row = Instantiate(blockData.PrefabRow, transform);
                List<GridResponseData> responses = blockData.Responses.GetRange(startIndex, endIndex - startIndex);
                row.Populate(blockData.Responses.GetRange(startIndex, endIndex - startIndex), _toggleGroup);
                _bodyRows.Add(row);
            }

            UpdateLayout();
        }

        public void UpdateLayout()
        {
            Canvas.ForceUpdateCanvases();

            _header.UpdateLayout();

            foreach (var row in _bodyRows)
            {
                row.UpdateLayout();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }
}