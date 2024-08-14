using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Survey
{
    public class SurveyOutput : MonoBehaviour
    {
        [SerializeField] private bool overwriteOutputOnEditorPlay;
        [SerializeField] private List<SurveyOutputData> outputData;

        private delegate string GetValueFromData(SurveyOutputData outputData);
        private const string VerboseFileName = "restaurant_output_verbose";
        private const string TerseFileName = "restaurant_output_terse";

        public void AddToTrialDataBuffer (List<SurveyOutputData> data)
        {
            outputData.AddRange(data);
        }

        public void ClearTrialDataBuffer()
        {
            outputData.Clear();
        }

        [Button("Write to disk")]
        public void WriteTrialDataBufferToDisk()
        {
            RecordData(VerboseFileName, GetVerboseKey, GetVerboseValue);
            RecordData(TerseFileName, GetTerseKey, GetTerseValue);
        }

        private void RecordData(string fileName, GetValueFromData getKey, GetValueFromData getValue)
        {
            string filePath = GetFilePath(fileName);

            Debug.Log($"Writing output data to {filePath}...");

            bool fileExists = File.Exists(filePath);

            // Clear data during development
            if (Application.isEditor && overwriteOutputOnEditorPlay && File.Exists(filePath))
            {
                Debug.Log($"Deleting existing {fileName} file");
                File.Delete(filePath);
                fileExists = false;
            }

            using (var writer = new StreamWriter(filePath, true))
            {
                if (!fileExists)
                {
                    Debug.Log($"Creating new {fileName} file");

                    // Create the header row
                    var values = outputData.Select(i => getKey(i));
                    writer.WriteLine(string.Join(",", values));
                }
                else
                {
                    Debug.Log($"Appending data to existing {fileName} file");
                }

                // Create the new response row
                var responseValues = outputData.Select(i => getValue(i));
                writer.WriteLine(string.Join(",", responseValues));
            }

            Debug.Log($"{fileName} output data written");
        }

        private string GetFilePath(string fileName)
        {
            return $"{Application.streamingAssetsPath}/{fileName}.csv";
        }

        private string GetVerboseKey(SurveyOutputData outputData)
        {
            return $"\"{outputData.VerboseKey}\"";
        }

        private string GetVerboseValue(SurveyOutputData outputData)
        {
            return $"\"{outputData.VerboseValue}\"";
        }

        private string GetTerseKey(SurveyOutputData outputData)
        {
            return $"\"{outputData.TerseKey}\"";
        }

        private string GetTerseValue(SurveyOutputData outputData)
        {
            return $"\"{outputData.TerseValue}\"";
        }
    }
}