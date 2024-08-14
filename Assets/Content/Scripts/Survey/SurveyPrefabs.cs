using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survey
{
    using Grid;
    using Info;
    using Survey.Likert;

    [CreateAssetMenu(fileName = "Survey Prefabs", menuName = "Survey/Prefabs")]
    public class SurveyPrefabs : ScriptableObject
    {
        [field: SerializeField] public InfoBlock InfoBlock { get; private set; }
        [field: SerializeField] public GridBlock GridBlock { get; private set; }
        [field: SerializeField] public LikertBlock LikertBlock { get; private set; }
    }
}