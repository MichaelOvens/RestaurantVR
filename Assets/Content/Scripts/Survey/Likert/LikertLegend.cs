using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Survey.Likert
{
    public class LikertLegend : MonoBehaviour
    {
        public float PreferredHeight => _labels.Select(i => i.preferredHeight).Max();

        [SerializeField] private TextMeshProUGUI _prefabLabel;

        private List<TextMeshProUGUI> _labels = new List<TextMeshProUGUI>();

        public void Populate(List<string> responseLabels)
        {
            foreach (var response in responseLabels)
            {
                var label = Instantiate(_prefabLabel, transform);
                label.text = response;
                _labels.Add(label);
            }

            if (_labels.Count >= 2)
            {
                var leftLabel = _labels.First();
                leftLabel.horizontalAlignment = HorizontalAlignmentOptions.Left;
                leftLabel.margin = new Vector4()
                {
                    w = leftLabel.margin.w,     // Bottom
                    x = leftLabel.margin.x,     // Left
                    y = leftLabel.margin.y,     // Top
                    z = 0f,                     // Right
                };

                var rightLabel = _labels.Last();
                rightLabel.horizontalAlignment = HorizontalAlignmentOptions.Right;
                rightLabel.margin = new Vector4()
                {
                    w = rightLabel.margin.w,    // Bottom
                    x = 0f,                     // Left
                    y = rightLabel.margin.y,    // Top
                    z = rightLabel.margin.z,    // Right
                };

                for (int i = 1; i < _labels.Count - 1; i++) 
                {
                    var middleLabel = _labels[i];
                    middleLabel.horizontalAlignment = HorizontalAlignmentOptions.Center;
                    middleLabel.margin = new Vector4()
                    {
                        w = middleLabel.margin.w,   // Bottom
                        x = 0f,                     // 
                        y = middleLabel.margin.y,   // Top
                        z = 0f,                     
                    };
                }
            }
        }
    }
}