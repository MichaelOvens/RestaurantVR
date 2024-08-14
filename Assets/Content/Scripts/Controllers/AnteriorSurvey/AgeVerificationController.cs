using Survey;
using Survey.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class AgeVerificationController : MonoBehaviour
    {
        public event Action<List<SurveyOutputData>,bool> OnAgeVerificationComplete;

        [SerializeField] private SurveyManager _surveyManager;
        [SerializeField] private List<SurveyPageData> _ageVerificationPage;
        [SerializeField] private GridBlockData _ageVerificationQuestion;
       
        public void StartAgeVerification()
        {
            _surveyManager.OnSurveyComplete += OnSurveyComplete;
            _surveyManager.StartSurvey(_ageVerificationPage);
        }

        private void OnSurveyComplete(List<SurveyOutputData> outputData)
        {
            _surveyManager.OnSurveyComplete -= OnSurveyComplete;

            bool userIsUnder18 = !_ageVerificationQuestion.Responses[0].selected;
            OnAgeVerificationComplete?.Invoke(outputData, userIsUnder18);
        }
    }
}
