using Survey.Grid;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Survey.Likert
{
    public class LikertHeaderRow : MonoBehaviour
    {
        [SerializeField] private LikertLegend _legend;
        [SerializeField] private TextMeshProUGUI _textElement;
        [SerializeField] private RectTransform _rectTransform;

        public void Populate(string headerText, List<string> responseLabels)
        {
            _textElement.text = headerText;
            _legend.Populate(responseLabels);
        }

        public void UpdateLayout()
        {
            var preferredHeights = new List<float>()
            {
                _textElement.preferredHeight,
                _legend.PreferredHeight
            };

            _rectTransform.sizeDelta = new Vector2()
            {
                x = _rectTransform.sizeDelta.x,
                y = preferredHeights.Max()
            };
        }
    }
}