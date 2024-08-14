using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Survey.Grid
{
    [CreateAssetMenu(fileName = "Grid Block", menuName = "Survey/Grid Block")]
    public class GridBlockData : SurveyItem
    {
        public GridBlockType Type;
        public bool AllowNullSelection;
        public GridBodyRow PrefabRow;
        public GridQuestionData Question;
        public List<GridResponseData> Responses;

        public override GameObject Instantiate(SurveyPrefabs prefabs, Transform parent)
        {
            var block = Instantiate(prefabs.GridBlock, parent);
            block.Populate(this);
            return block.gameObject;
        }

        public override bool IsBlockingContinueButton()
        {
            if (AllowNullSelection) return false;
            
            // We only need one response to continue
            foreach (var response in Responses)
            {
                if (response.selected)
                {
                    return false;
                }
            }
            
            return true;
        }

        public override List<SurveyOutputData> GetResponses()
        {
            var responses = new List<SurveyOutputData>();

            if (Type == GridBlockType.SingleResponse)
            {
                GridResponseData selectedResponse = Responses.FirstOrDefault(i => i.selected);

                if (selectedResponse != null)
                {
                    responses.Add(new SurveyOutputData()
                    {
                        VerboseKey = $"{Question.Id}: {Question.Text}",
                        VerboseValue = $"{selectedResponse.id}: {selectedResponse.text}",

                        TerseKey = $"{Question.Id}",
                        TerseValue = $"{selectedResponse.id}"
                    });
                }
                else
                {
                    responses.Add(new SurveyOutputData()
                    {
                        VerboseKey = $"{Question.Id}: {Question.Text}",
                        VerboseValue = $"0: No response",

                        TerseKey = $"{Question.Id}",
                        TerseValue = $"0"
                    });
                }
            }
            else if (Type == GridBlockType.MultiResponse)
            {
                foreach (var response in Responses)
                {
                    string responseNumber = response.selected ? "1" : "0";
                    string responseText = response.selected ? "True" : "False";

                    responses.Add(new SurveyOutputData()
                    {
                        VerboseKey = $"{Question.Id}.{response.id}: {Question.Text} | {response.text}",
                        VerboseValue = $"{responseNumber}: {responseText}",

                        TerseKey = $"{Question.Id}",
                        TerseValue = $"{responseText}"
                    });
                }
            }

            return responses;
        }
    }
}