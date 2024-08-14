using TMPro;
using UnityEngine;

namespace Survey.Grid
{
    public class GridHeaderRow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textElement;
        [SerializeField] private RectTransform rectTransform;

        public void Populate (GridBlockData blockData)
        {
            textElement.text = blockData.Question.Text;
        }

        public void UpdateLayout()
        {
            rectTransform.sizeDelta = new Vector2()
            {
                x = rectTransform.sizeDelta.x,
                y = textElement.preferredHeight
            };
        }
    }
}