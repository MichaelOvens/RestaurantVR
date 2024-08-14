using NaughtyAttributes;
using Restaurant;
using System.Collections.Generic;
using UnityEngine;

namespace MenuSurvey 
{
    [CreateAssetMenu(fileName = "Menu Data", menuName = "Survey/Menu Data")]
    public class MenuData : ScriptableObject
    {
        [Header("Starters")]
        [SerializeField] private MenuItemData _starter_1;
        [SerializeField] private MenuItemData _starter_2;
        [SerializeField] private MenuItemData _starter_3;
        [SerializeField] private MenuItemData _starter_4;

        [Header("Mains")]
        [SerializeField] private MenuItemData _main_1;
        [SerializeField] private MenuItemData _main_2;
        [SerializeField] private MenuItemData _main_3_control;
        [SerializeField] private MenuItemData _main_3_variant;
        [SerializeField] private MenuItemData _main_3_taste;
        [SerializeField] private MenuItemData _main_4;
        [SerializeField] private MenuItemData _main_5_control;
        [SerializeField] private MenuItemData _main_5_variant;
        [SerializeField] private MenuItemData _main_5_taste;
        [SerializeField] private MenuItemData _main_6;

        public List<MenuItemData> GetStarters()
        {
            return new List<MenuItemData>()
            {
                _starter_1,
                _starter_2,
                _starter_3,
                _starter_4,
            };
        }

        public List<MenuItemData> GetMains(
            TrialConditions.MeatCondition meatCondition,
            TrialConditions.VegeCondition vegeCondition)
        {
            return new List<MenuItemData>()
            {
                _main_1,
                _main_2,
                GetMeatCondition(meatCondition),
                _main_4,
                GetVegeConditions(vegeCondition),
                _main_6
            };
        }

        private MenuItemData GetMeatCondition(TrialConditions.MeatCondition meatCondition)
        {
            switch (meatCondition)
            {
                case TrialConditions.MeatCondition.Control:
                    return _main_3_control;
                case TrialConditions.MeatCondition.Variant:
                    return _main_3_variant;
                case TrialConditions.MeatCondition.Taste:
                    return _main_3_taste;
                default:
                    throw new System.IndexOutOfRangeException("Meat condition not recognised");
            }
        }

        private MenuItemData GetVegeConditions(TrialConditions.VegeCondition vegeCondition)
        {
            switch (vegeCondition)
            {
                case TrialConditions.VegeCondition.Control:
                    return _main_5_control;
                case TrialConditions.VegeCondition.More:
                    return _main_5_variant;
                case TrialConditions.VegeCondition.Taste:
                    return _main_5_taste;
                default:
                    throw new System.IndexOutOfRangeException("Vege condition not recognised");
            }
        }
    }
}