using System;
using System.Collections;
using System.Collections.Generic;
using Interaction.UI;
using UnityEngine;

namespace Survey.Likert
{
    public class LikertResponses : MonoBehaviour
    {
        public EventHandler<int> OnSelectedResponseChanged;

        [SerializeField] private UIToggleGroup toggleGroup;
        [SerializeField] private List<LikertResponseToggle> responseButtons;

        [Header("Prefabs")]
        [SerializeField] private LikertResponseToggle prefabButton;

        private int _selectedResponse;

        public void Populate(LikertResponseData responseData)
        {
            _selectedResponse = 0;

            for (int i = 0; i < responseData.ResponseCount; i++)
            {
                var response = Instantiate(prefabButton, transform);
                response.Populate(toggleGroup, i + 1);
                response.OnToggleSelected += OnToggleSelected;
                response.OnToggleDeselected += OnToggleDeselected;
                responseButtons.Add(response);
            }
        }

        private void OnToggleSelected(object sender, int responseId)
        {
            if (responseId != _selectedResponse)
            {
                _selectedResponse = responseId;
                OnSelectedResponseChanged.Invoke(this, _selectedResponse);
            }
        }

        private void OnToggleDeselected(object sender, int responseId)
        {
            if (responseId == _selectedResponse)
            {
                _selectedResponse = 0;
                OnSelectedResponseChanged.Invoke(this, _selectedResponse);
            }
        }
    }
}