using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Survey;

namespace MenuSurvey
{
    [System.Serializable]
    public class MenuItemVariable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Text { get; private set; }

        [SerializeField] private List<string> options;

    }
}