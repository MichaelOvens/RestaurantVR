using Controllers.Scenario;
using Interaction;
using MenuSurvey;
using Player;
using Restaurant;
using Survey;
using System;
using System.Collections.Generic;
using Trial;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Responsible for loading a scenario, navigating player to a seat, and interacting with the menu.
    /// </summary>
    public class ScenarioController : MonoBehaviour
    {
        [SerializeField] private Transform _startTarget;
        [SerializeField] private MenuSpawner _menuSpawner;
        [SerializeField] private MenuVariantSelector _menuVariantSelector;
        [SerializeField] private PosteriorSurveyController _posteriorSurveyController;

        private Menu _menuInstance = null;

        private void Start()
        {
            PlayerManager.Instance.MoveTo(_startTarget);
            PlayerManager.Instance.Vignette.LowerVignette();

            StartMenuSpawn();
        }

        private void StartMenuSpawn()
        {
            _menuSpawner.OnMenuSpawned += OnMenuSpawnComplete;
            _menuSpawner.StartListeningForTrigger();
        }

        private void OnMenuSpawnComplete(Menu menu)
        {
            _menuSpawner.OnMenuSpawned -= OnMenuSpawnComplete;
            _menuInstance = menu;

            StartMenuOpen();
        }

        private void StartMenuOpen()
        {
            MenuData menuVariant = _menuVariantSelector.GetMenuData(TrialManager.Conditions.MenuType);
            _menuInstance.Populate(menuVariant, TrialManager.Conditions);

            _menuInstance.GetComponent<WorldSurveyAnimator>().OnSurveyOpenStart += OnMenuOpenComplete;
        }

        private void OnMenuOpenComplete()
        {
            _menuInstance.GetComponent<WorldSurveyAnimator>().OnSurveyOpenStart -= OnMenuOpenComplete;

            TrialManager.AddTimestampToTrialBuffer("Menu Opened");

            StartMenuSelection();
        }

        private void StartMenuSelection()
        {
            _menuInstance.OnMenuSelectionComplete += OnMenuSelectionComplete;
        }

        private void OnMenuSelectionComplete(List<SurveyOutputData> outputData)
        {
            _menuInstance.OnMenuSelectionComplete += OnMenuSelectionComplete;

            TrialManager.AddTimestampToTrialBuffer("Menu Selection");
            TrialManager.AddDataToTrialBuffer(outputData);

            TransitionToPosteriorSurvey();
        }

        private void TransitionToPosteriorSurvey()
        {
            var menuInteractables = _menuInstance.GetComponentsInChildren<Interactable>();
            foreach (var interactable in menuInteractables)
            {
                interactable.IsInteractable = false;
            }

            PlayerManager.Instance.Vignette.OnVignetteRaiseComplete += OnTransitionToPosteriorSurveyComplete;
            PlayerManager.Instance.Vignette.RaiseVignette();
        }

        private void OnTransitionToPosteriorSurveyComplete()
        {
            PlayerManager.Instance.Vignette.OnVignetteRaiseComplete -= OnTransitionToPosteriorSurveyComplete;

            Transform surveyTarget = _menuInstance.transform.parent;

            Destroy(_menuInstance.gameObject);
            _menuInstance = null;

            _posteriorSurveyController.StartPosteriorSurvey(surveyTarget);
        }
    }
}
