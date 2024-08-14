using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Survey.Likert
{
    public class LikertQuestion : MonoBehaviour
    {
        public float PreferredHeight => _textElement.preferredHeight;

        [SerializeField] private TextMeshProUGUI _textElement;

        public void UpdateLayout()
        {
            // Do nothing
        }

        public void Populate(string questionText)
        {
            _textElement.text = questionText;
        }
    }
}