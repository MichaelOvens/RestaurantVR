using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survey.Info
{
    public class InfoBlock : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _textElement;

        public void Populate(InfoData data)
        {
            _textElement.text = data.Text;

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }
}