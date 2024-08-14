using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    public abstract class SurveyItem : ScriptableObject
    {
        public abstract GameObject Instantiate(SurveyPrefabs prefabs, Transform parent);
        public abstract bool IsBlockingContinueButton();
        public abstract List<SurveyOutputData> GetResponses();
    }
}