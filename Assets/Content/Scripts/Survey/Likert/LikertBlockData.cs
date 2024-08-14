using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Survey.Likert
{
    [CreateAssetMenu(fileName = "Likert Block", menuName = "Survey/Likert Block")]
    public class LikertBlockData : SurveyItem
    {
        public string BlockId;
        public string HeaderText;
        public List<LikertQuestionData> Questions;
        public LikertResponseData Responses;

        public override GameObject Instantiate(SurveyPrefabs prefabs, Transform parent)
        {
            var block = Instantiate(prefabs.LikertBlock, parent);
            block.Populate(this);
            return block.gameObject;
        }

        public override bool IsBlockingContinueButton()
        {
            foreach (var question in Questions)
            {
                if (question.SelectedResponse <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override List<SurveyOutputData> GetResponses()
        {
            var responses = new List<SurveyOutputData>();

            foreach (var question in Questions)
            {
                responses.Add(new SurveyOutputData()
                {
                    VerboseKey = $"{BlockId}.{question.Id}: {HeaderText} | {question.Text}",
                    VerboseValue = $"{question.SelectedResponse} (Range: 1-{Responses.ResponseCount}) (Labels: {string.Join(", ", Responses.Labels)})",

                    TerseKey = $"{BlockId}.{question.Id}",
                    TerseValue = $"{question.SelectedResponse}"
                });
            }

            return responses;
        }
    }
}