using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    [CreateAssetMenu(fileName = "Survey Page", menuName = "Survey/Survey Page")]
    public class SurveyPageData : ScriptableObject
    {
        public EventHandler OnPageComplete;

        public List<SurveyItem> Blocks;
    }
}