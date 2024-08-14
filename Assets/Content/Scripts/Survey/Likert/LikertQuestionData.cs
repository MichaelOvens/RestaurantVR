using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survey.Likert
{
    [System.Serializable]
    public class LikertQuestionData
    {
        public string Id;
        public string Text;
        public int SelectedResponse;
    }
}