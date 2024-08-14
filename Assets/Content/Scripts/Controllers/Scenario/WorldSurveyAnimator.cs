using System;
using UnityEngine;
using Interaction;
using Interaction.Effects;

namespace Controllers.Scenario
{
    public class WorldSurveyAnimator : MonoBehaviour, IOnStateMachineStateExit
    {
        public event Action OnSurveyOpenStart, OnSurveyOpenComplete;

        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerName;
        [SerializeField] private string _stateLabel;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Interactable _interactable;
        [SerializeField] private Interactable _continueButton;

        private void Awake()
        {
            _interactable.OnSelected += OnSelected;
        }

        private void Start()
        {
            // Disable all interactables to avoid accidental menu selections
            var childInteractables = GetComponentsInChildren<Interactable>();
            foreach (var child in childInteractables)
                child.IsInteractable = false;

            // Re-enable the open interactable
            _interactable.IsInteractable = true;
            _particleSystem.Play();
        }

        // Called when the survey starts to open
        private void OnSelected(object sender, EventArgs e)
        {
            _interactable.IsInteractable = false;
            _particleSystem.Stop();
            
            _animator.SetTrigger(_triggerName);

            OnSurveyOpenStart?.Invoke();
        }

        // Called when the survey has been opened
        public void OnStateMachineStateExit(string label)
        {
            if (label != _stateLabel) return;

            // Re-enable all interactables so we can select menu items
            var childInteractables = GetComponentsInChildren<Interactable>();
            foreach (var child in childInteractables)
                child.IsInteractable = true;

            // Make sure the open interactable is still disabled
            _interactable.IsInteractable = false;

            // The continue button should also default to disabled until a selection is made
            _continueButton.IsInteractable = false;

            OnSurveyOpenComplete?.Invoke();
        }
    }
}
