using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    public class SurveyManager : MonoBehaviour
    {
        public event Action<List<SurveyOutputData>> OnSurveyComplete;
        
        [SerializeField] private SurveyPrefabs prefabData;
        [SerializeField] private SurveyPageManager pageManager;

        private int _pageIndex = 0;
        private List<SurveyPageData> _pages;
        private List<SurveyOutputData> _outputData;

        private void Awake()
        {
            pageManager.OnPageComplete += OnPageComplete;
        }

        public void StartSurvey(SurveyPageData page)
        {
            StartSurvey(new List<SurveyPageData>() { page });
        }

        public void StartSurvey(List<SurveyPageData> pages)
        {
            _pageIndex = 0;
            _pages = pages;
            _outputData = new List<SurveyOutputData>();

            pageManager.LoadPage(prefabData, _pages[_pageIndex]);
        }

        public void ClearSurvey()
        {
            _pageIndex = 0;
            _pages = new List<SurveyPageData>();
            _outputData = new List<SurveyOutputData>();

            pageManager.Clear();
        }

        public void LoadPreviousPage()
        {
            _pageIndex = Mathf.Clamp(_pageIndex - 1, 0, _pages.Count);
            pageManager.LoadPage(prefabData, _pages[_pageIndex]);
        }

        public void LoadNextPage()
        {
            _pageIndex = Mathf.Clamp(_pageIndex + 1, 0, _pages.Count);

            if (_pageIndex < _pages.Count)
            {
                pageManager.LoadPage(prefabData, _pages[_pageIndex]);
            }
            else
            {
                OnSurveyComplete?.Invoke(_outputData);
            }
        }

        private void OnPageComplete(object sender, SurveyPageData pageData)
        {
            Debug.Log("Page completed");

            // Record data from the completed page
            foreach (var question in pageData.Blocks)
            {
                _outputData.AddRange(question.GetResponses());
            }

            pageManager.Clear();

            LoadNextPage();
        }
   }
}