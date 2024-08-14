using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    public class SurveyLoader : MonoBehaviour
    {
        [SerializeField] private SurveyManager surveyManager;
        [SerializeField] private List<SurveyPageData> pageData;

        private void Start()
        {
            surveyManager.StartSurvey(pageData);
        }

        [Button("Load Next Page")]
        public void LoadNextPage()
        {
            surveyManager.LoadNextPage();
        }

        [Button("Load Previous Page")]
        public void LoadPreviousPage()
        {
            surveyManager.LoadNextPage();
        }
    }
}