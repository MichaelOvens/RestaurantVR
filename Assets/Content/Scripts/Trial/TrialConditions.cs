using Survey;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Restaurant
{
    [System.Serializable]
    public class TrialConditions
    {
        public enum EducationCondition { Control, Experimental }
        public enum MenuCondition { Casual1, Casual2, Fine1, Fine2 }
        public enum MeatCondition { Control, Variant, Taste }
        public enum VegeCondition { Control, More, Taste }

        public EducationCondition Education => _education;
        public MenuCondition MenuType => _menuType;
        public MeatCondition Meat => _meat;
        public VegeCondition Vege => _vege;

        [SerializeField] private EducationCondition _education;
        [SerializeField] private MenuCondition _menuType;
        [SerializeField] private MeatCondition _meat;
        [SerializeField] private VegeCondition _vege;

        public TrialConditions(
            EducationCondition education,
            MenuCondition menuType,
            MeatCondition meat,
            VegeCondition vege)
        {
            _education = education;
            _menuType = menuType;
            _meat = meat;
            _vege = vege;
        }

        public bool IsConditionActive(string condition)
        {
            var activeConditions = new List<string>()
            {
                GetConditionName<EducationCondition>(Education.ToString()),
                GetConditionName<MenuCondition>(MenuType.ToString()),
                GetConditionName<MeatCondition>(Meat.ToString()),
                GetConditionName<VegeCondition>(Vege.ToString()),
            };

            return string.IsNullOrWhiteSpace(condition) || activeConditions.Contains(condition);
        }

        public List<SurveyOutputData> GetConditionsAsOutputData()
        {
            return new List<SurveyOutputData>()
            {
                new SurveyOutputData()
                {
                    VerboseKey = "EducationCondition",
                    VerboseValue = GetConditionName<EducationCondition>(Education.ToString()),

                    TerseKey = "EducationCondition",
                    TerseValue = Education.ToString()
                },
                new SurveyOutputData()
                {
                    VerboseKey = "MenuCondition",
                    VerboseValue = GetConditionName<MenuCondition>(MenuType.ToString()),

                    TerseKey = "MenuCondition",
                    TerseValue = MenuType.ToString()
                },
                new SurveyOutputData()
                {
                    VerboseKey = "MeatCondition",
                    VerboseValue = GetConditionName<MeatCondition>(Meat.ToString()),

                    TerseKey = "MeatCondition",
                    TerseValue = Meat.ToString()
                },
                new SurveyOutputData()
                {
                    VerboseKey = "VegeCondition",
                    VerboseValue = GetConditionName<VegeCondition>(Vege.ToString()),

                    TerseKey = "VegeCondition",
                    TerseValue = Vege.ToString()
                },
            };
        }

        private string GetConditionName<T>(string value)
        {
            // Remove the namespace qualifiers from the type name,
            // ie. Experiment.TrialConditions+EducationCondition -> EducationCondition
            string condition = Regex.Match(typeof(T).ToString(), @"\+.*").Value.Substring(1);

            return condition + "." + value;
        }
    }
}