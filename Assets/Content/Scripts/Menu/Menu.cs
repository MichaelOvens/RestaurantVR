using Interaction.UI;
using Restaurant;
using Survey;
using Survey.Grid;
using Survey.Likert;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSurvey
{
    public class Menu : MonoBehaviour
    {
        public event Action<List<SurveyOutputData>> OnMenuSelectionComplete;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _facingPageRectTransform;
        [SerializeField] private LikertBlockData _mainAppealData;
        [SerializeField] private List<MenuSelectionRow> _starterRows;
        [SerializeField] private List<MenuSelectionRow> _mainRows;
        [SerializeField] private UIButton _continueButton;

        private delegate void ToggleEvent(object sender, MenuItemData toggledRow);

        private MenuItemData _selectedStarter = null;
        private MenuItemData _selectedMain = null;

        private void Awake()
        {
            foreach (var row in _starterRows)
                row.OnSelectionToggled += OnStarterRowToggled;

            foreach (var row in _mainRows)
                row.OnSelectionToggled += OnMainRowToggled;

            _continueButton.OnButtonClicked += OnContinueButtonClicked;
        }

        public void Populate(MenuData menuData, TrialConditions trialConditions)
        {
            var starterData = menuData.GetStarters();
            var mainData = menuData.GetMains(trialConditions.Meat, trialConditions.Vege);

            PopulateRows(_starterRows, starterData);
            PopulateRows(_mainRows, mainData);

            PopulateData(mainData);

            _continueButton.IsInteractable = false;

            UpdateLayout();
        }

        private void PopulateRows(List<MenuSelectionRow> rows,  List<MenuItemData> items)
        {
            if (rows.Count != items.Count)
                throw new System.IndexOutOfRangeException($"Data has {items.Count} entries, but the menu has {rows.Count} rows");

            for (int i = 0; i < rows.Count; i++)
            {
                items[i].IsSelected = false;
                rows[i].Populate(items[i]);
            }
        }

        private void PopulateData (List<MenuItemData> mainItems)
        {
            Debug.Log($"Populating {mainItems.Count} main menu items");

            _mainAppealData.Questions.Clear();
            for (int i = 0; i < mainItems.Count; i++)
            {
                Debug.Log($"Adding {mainItems[i].Text}");
                _mainAppealData.Questions.Add(new LikertQuestionData()
                {
                    Id = (i + 1).ToString(),
                    Text = mainItems[i].Text,
                    SelectedResponse = 0
                });
            }
        }

        private void UpdateLayout()
        {
            Canvas.ForceUpdateCanvases();

            foreach (var row in _starterRows)
            {
                row.UpdateLayout();
            }

            foreach (var row in _mainRows)
            {
                row.UpdateLayout();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            Canvas.ForceUpdateCanvases();

            _facingPageRectTransform.sizeDelta = new Vector2()
            {
                x = _facingPageRectTransform.sizeDelta.x,
                y = _rectTransform.sizeDelta.y
            };
        }

        private void OnStarterRowToggled(object sender, MenuItemData toggledRow)
        {
            if (toggledRow.IsSelected)
            {
                _selectedStarter = toggledRow;
            }
            else if (toggledRow == _selectedStarter)
            {
                _selectedStarter = null;
            }
        }

        private void OnMainRowToggled(object sender, MenuItemData toggledRow)
        {
            if (toggledRow.IsSelected)
            {
                _selectedMain = toggledRow;
            }
            else if (toggledRow == _selectedMain)
            {
                _selectedMain = null;
            }

            _continueButton.IsInteractable = _selectedMain != null;
        }

        private void OnContinueButtonClicked(object sender, EventArgs e)
        {
            var outputData = new List<SurveyOutputData>()
            {
                new SurveyOutputData()
                {
                    VerboseKey = "Starter",
                    VerboseValue = GetVerboseText(_selectedStarter),

                    TerseKey = "Starter",
                    TerseValue = GetTerseText(_selectedStarter)
                },
                new SurveyOutputData()
                {
                    VerboseKey = "Main",
                    VerboseValue = GetVerboseText(_selectedMain),

                    TerseKey = "Main",
                    TerseValue = GetTerseText(_selectedMain)
                }
            };

            OnMenuSelectionComplete?.Invoke(outputData);
        }

        private string GetVerboseText(MenuItemData itemData)
        {
            if (itemData != null)
            {
                return $"{itemData.Id}: {itemData.Text}";
            }
            else
            {
                return "None";
            }
        }

        private string GetTerseText(MenuItemData itemData)
        {
            if (itemData != null)
            {
                return itemData.Id.ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}