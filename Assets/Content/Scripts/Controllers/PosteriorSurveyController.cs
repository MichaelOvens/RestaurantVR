using Controllers.Scenario;
using Player;
using Survey;
using System;
using System.Collections;
using System.Collections.Generic;
using Trial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Restaurant
{
    public class PosteriorSurveyController : MonoBehaviour
    {
        [SerializeField] private SurveyManager _prefabPosteriorSurvey;
        [SerializeField] private List<SurveyPageData> _surveyPages;
        [SerializeField] private string _resetTrialScence;

        private SurveyManager _surveyInstance = null;

        public void StartPosteriorSurvey(Transform surveyTarget)
        {
            _surveyInstance = Instantiate(_prefabPosteriorSurvey, surveyTarget);
            _surveyInstance.ClearSurvey();

            _surveyInstance.GetComponent<WorldSurveyAnimator>().OnSurveyOpenComplete += OnSurveyOpenAnimationComplete;

            PlayerManager.Instance.Vignette.LowerVignette();
        }

        private void OnSurveyOpenAnimationComplete()
        {
            _surveyInstance.GetComponent<WorldSurveyAnimator>().OnSurveyOpenComplete -= OnSurveyOpenAnimationComplete;

            LoadPosteriorSurveyData();
        }

        private void LoadPosteriorSurveyData()
        {
            _surveyInstance.OnSurveyComplete += OnPosteriorSurveyComplete;
            _surveyInstance.StartSurvey(_surveyPages);
        }

        private void OnPosteriorSurveyComplete(List<SurveyOutputData> outputData)
        {
            _surveyInstance.OnSurveyComplete -= OnPosteriorSurveyComplete;
            TrialManager.AddDataToTrialBuffer(outputData);
            TrialManager.AddTimestampToTrialBuffer("End");
            TrialManager.EndTrial();

            SceneManager.LoadScene(_resetTrialScence);
        }
    }
}
