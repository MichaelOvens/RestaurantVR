using Restaurant;
using Survey;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trial
{
    public class TrialManager : MonoBehaviour
    {
        public static TrialConditions Conditions => _instance._conditions;
        
        private static TrialManager _instance = null;
        [SerializeField] private TrialConditions _conditions;
        [SerializeField] private TrialData _data;

        [Header("Development")]
        [SerializeField] private bool _randomiseInEditMode = true;

        public static void StartNewTrial() 
        { 
            _instance._StartNewTrial(); 
        }

        public static void AddTimestampToTrialBuffer(string label)
        {
            AddDataToTrialBuffer(new SurveyOutputData()
            {
                VerboseKey = $"{label} Timestamp",
                VerboseValue = DateTime.Now.ToString(),

                TerseKey = $"{label}",
                TerseValue = DateTime.Now.ToString()
            });
        }

        public static void AddDataToTrialBuffer(SurveyOutputData outputData)
        {
            AddDataToTrialBuffer(new List<SurveyOutputData>() { outputData });
        }

        public static void AddDataToTrialBuffer (List<SurveyOutputData> outputData)
        {
            _instance._data.AddToTrialDataBuffer(outputData);
        }

        public static void EndTrial(bool recordData = true)
        {
            _instance._EndTrial(recordData);
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public void _StartNewTrial()
        {
            _data.ClearTrialDataBuffer();

            if (!Application.isEditor || _randomiseInEditMode)
            {
                _conditions = TrialRandomiser.GenerateRandomConditions();
            }

            _data.AddToTrialDataBuffer(_conditions.GetConditionsAsOutputData());
        }

        public void _EndTrial(bool recordData)
        {
            if (recordData)
            {
                _data.WriteTrialDataBufferToDisk();
            }

            _data.ClearTrialDataBuffer();
        }
    }
}
