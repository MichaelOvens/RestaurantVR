using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction.UI;

namespace Survey
{
    public class SurveyPageManager : MonoBehaviour
    {
        public EventHandler<SurveyPageData> OnPageComplete;

        [SerializeField] private UIButton continueButton;
        
        private SurveyPageData _currentPageData = null;
        private List<GameObject> _blocks = new List<GameObject>();

        public void LoadPage (SurveyPrefabs prefabs, SurveyPageData pageData)
        {
            Clear();

            gameObject.SetActive(true);

            _currentPageData = pageData;

            foreach (var question in pageData.Blocks)
            {
                var block = question.Instantiate(prefabs, transform);
                _blocks.Add(block);
            }

            continueButton.gameObject.SetActive(true);
            continueButton.transform.SetAsLastSibling();
            continueButton.OnButtonClicked += OnContinueButtonClicked;
        }

        public void Clear()
        {
            _currentPageData = null;

            foreach (var block in _blocks)
            {
                Destroy(block.gameObject);
            }

            _blocks.Clear();
            continueButton.gameObject.SetActive(false);
        }

        private void Update()
        {
            UpdateContinueButtonInteractableState();
        }

        private void UpdateContinueButtonInteractableState()
        {
            if (_currentPageData == null)
            {
                continueButton.IsInteractable = false;
                return;
            }

            bool continueButtonEnabled = true;

            foreach (var question in _currentPageData.Blocks)
            {
                if (question.IsBlockingContinueButton())
                {
                    continueButtonEnabled = false;
                    break;
                }
            }

            continueButton.IsInteractable = continueButtonEnabled;
        }

        private void OnContinueButtonClicked(object sender, EventArgs e)
        {
            continueButton.OnButtonClicked -= OnContinueButtonClicked;
            OnPageComplete.Invoke(this, _currentPageData);
        }
    }
}