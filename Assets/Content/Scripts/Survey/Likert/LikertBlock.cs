using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survey.Likert
{
    public class LikertBlock : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private LikertHeaderRow _headerRow;
        [SerializeField] private List<LikertBodyRow> _bodyRows;

        [Header("Prefabs")]
        [SerializeField] private LikertBodyRow _prefabBodyRow;

        public void Populate (LikertBlockData blockData)
        {
            // Set all the responses to 0, indicating no response selected
            foreach (var question in blockData.Questions)
            {
                question.SelectedResponse = 0;
            }

            _headerRow.Populate(blockData.HeaderText, blockData.Responses.Labels);

            foreach (var question in blockData.Questions)
            {
                var row = Instantiate(_prefabBodyRow, transform);
                row.Populate(question, blockData.Responses);
                _bodyRows.Add(row);
            }

            UpdateLayout();
        }

        public void UpdateLayout()
        {
            Canvas.ForceUpdateCanvases();

            _headerRow.UpdateLayout();

            foreach (var row in _bodyRows)
            {
                row.UpdateLayout();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }
}