using Controllers.Anterior;
using Player;
using Restaurant;
using Survey;
using System.Collections.Generic;
using Trial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class AnteriorSurveyController : MonoBehaviour
    {
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private SurveyManager _surveyManager;
        [SerializeField] private List<SurveyPageData> _welcomePages;
        [SerializeField] private AgeVerificationController _ageVerification;
        [SerializeField] List<SurveyPageData> _beforeEducationPages;
        [SerializeField] EducationController _education;
        [SerializeField] List<SurveyPageData> _afterEducationPages;
        [SerializeField] private EnterRestaurantController _enterRestaurantController;

        [System.Serializable]
        private class RestaurantVariant
        {
            [field:SerializeField] public TrialConditions.MenuCondition Condition { get; private set; }
            [field: SerializeField] public string SceneName { get; private set; }
        }

        private void Start()
        {
            TrialManager.StartNewTrial();
            PlayerManager.Instance.MoveTo(_playerPosition);
            StartWelcomeMessage();
        }

        private void StartWelcomeMessage()
        {
            _surveyManager.OnSurveyComplete += OnWelcomePagesComplete;
            _surveyManager.StartSurvey(_welcomePages);
        }

        private void OnWelcomePagesComplete(List<SurveyOutputData> outputData)
        {
            _surveyManager.OnSurveyComplete -= OnWelcomePagesComplete;
            TrialManager.AddTimestampToTrialBuffer("Start");
            TrialManager.AddDataToTrialBuffer(outputData);

            StartAgeVerification();
        }

        private void StartAgeVerification()
        {
            _ageVerification.OnAgeVerificationComplete += OnAgeVerificationComplete;
            _ageVerification.StartAgeVerification();
        }

        private void OnAgeVerificationComplete(List<SurveyOutputData> outputData, bool userPassedAgeVerification)
        {
            _ageVerification.OnAgeVerificationComplete -= OnAgeVerificationComplete;
            TrialManager.AddDataToTrialBuffer(outputData);

            if (userPassedAgeVerification)
            {
                StartBeforeEducationPages();
            }
            else
            {
                SceneManager.LoadScene("Underage");
            }
        }

        private void StartBeforeEducationPages()
        {
            _surveyManager.OnSurveyComplete += OnBeforeEducationPagesComplete;
            _surveyManager.StartSurvey(_beforeEducationPages);
        }

        private void OnBeforeEducationPagesComplete(List<SurveyOutputData> outputData)
        {
            _surveyManager.OnSurveyComplete -= OnBeforeEducationPagesComplete;
            TrialManager.AddDataToTrialBuffer(outputData);

            StartEducation();
        }

        private void StartEducation()
        {
            _education.OnEducationComplete += OnEducationComplete;
            _surveyManager.gameObject.SetActive(false);
            _education.StartEducation();
        }

        private void OnEducationComplete()
        {
            _education.OnEducationComplete -= OnEducationComplete;
            _surveyManager.gameObject.SetActive(true);
            StartAfterEducationPages();
        }

        private void StartAfterEducationPages()
        {
            _surveyManager.OnSurveyComplete += OnAfterEducationPagesComplete;
            _surveyManager.StartSurvey(_afterEducationPages);
        }

        private void OnAfterEducationPagesComplete(List<SurveyOutputData> outputData)
        {
            _surveyManager.OnSurveyComplete -= OnAfterEducationPagesComplete;
            TrialManager.AddDataToTrialBuffer(outputData);
            _surveyManager.gameObject.SetActive(false);

            _enterRestaurantController.StartRestaurantTransition();
        }
    }
}
