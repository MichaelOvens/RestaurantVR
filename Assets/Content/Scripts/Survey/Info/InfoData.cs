using System.Collections.Generic;
using UnityEngine;

namespace Survey.Info
{
    [CreateAssetMenu(fileName = "Info Block", menuName = "Survey/Info Block")]
    public class InfoData : SurveyItem
    {
        [Multiline]
        public string Text;

        public override GameObject Instantiate(SurveyPrefabs prefabs, Transform parent)
        {
            var block = Instantiate(prefabs.InfoBlock, parent);
            block.Populate(this);
            return block.gameObject;
        }

        public override List<SurveyOutputData> GetResponses()
        {
            return new List<SurveyOutputData>();
        }

        public override bool IsBlockingContinueButton()
        {
            return false;
        }
    }
}