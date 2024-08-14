using Interaction.Effects;
using Interaction.UI;
using Survey.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Trial;
using UnityEngine;

namespace Controllers.Anterior
{
    public class EducationController : MonoBehaviour, IOnStateMachineStateExit
    {
        public event Action OnEducationComplete;

        [SerializeField] private Animator _animator;
        [SerializeField] private UIButton _continueButton;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private InfoData _controlData;
        [SerializeField] private InfoData _experimentalData;

        private const string OpenParameter = "isOpen";

        [NaughtyAttributes.Button("Start Education")]
        public void StartEducation()
        {
            switch (TrialManager.Conditions.Education)
            {
                case Restaurant.TrialConditions.EducationCondition.Control:
                    _textField.text = _controlData.Text;
                    break;
                case Restaurant.TrialConditions.EducationCondition.Experimental:
                    _textField.text = _experimentalData.Text;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException("Condition not recognised");
            }

            _animator.SetBool(OpenParameter, true);
        }

        private void OnContinueButtonClicked(object sender, EventArgs e)
        {
            _continueButton.OnButtonClicked -= OnContinueButtonClicked;
            _animator.SetBool(OpenParameter, false);
        }

        public void OnStateMachineStateExit(string label)
        {
            if (label == "onOpened")
            {
                _continueButton.OnButtonClicked += OnContinueButtonClicked;
            }

            if (label == "onClosed")
            {
                OnEducationComplete?.Invoke();
            }
        }
    }
}
