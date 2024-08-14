using NaughtyAttributes;
using UnityEngine;

namespace MenuSurvey
{
    [System.Serializable]
    public class MenuItemData
    {
        public int Id => _id;
        public string Text => _text;

        public bool IsSelected;

        [SerializeField] private int _id;
        [SerializeField][ResizableTextArea] private string _text;
    }
}