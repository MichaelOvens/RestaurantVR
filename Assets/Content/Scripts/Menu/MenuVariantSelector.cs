using Restaurant;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MenuSurvey
{
    [System.Serializable]
    public class MenuVariantSelector
    {
        [SerializeField] private List<MenuVariant> _menuVariants;

        [System.Serializable]
        private class MenuVariant
        {
            [field:SerializeField] public TrialConditions.MenuCondition Condition { get; private set; }
            [field: SerializeField] public MenuData Data { get; private set; }
        }

        public MenuData GetMenuData(TrialConditions.MenuCondition condition)
        {
            return _menuVariants.First(i => i.Condition == condition).Data;
        }
    }
}
