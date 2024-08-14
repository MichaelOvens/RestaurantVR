using System;
using System.Collections.Generic;

namespace Restaurant
{
    public static class TrialRandomiser
    {
        public static TrialConditions GenerateRandomConditions()
        {
            Random random = new Random();
            return new TrialConditions(
                GetRandom<TrialConditions.EducationCondition>(random),
                GetRandom<TrialConditions.MenuCondition>(random),
                GetRandom<TrialConditions.MeatCondition>(random),
                GetRandom<TrialConditions.VegeCondition>(random)
                );
        }

        public static List<TrialConditions> GenerateAllCombinations()
        {
            var conditions = new List<TrialConditions>();

            var educationConditions = Enum.GetValues(typeof(TrialConditions.EducationCondition));
            var menuTypeConditions = Enum.GetValues(typeof(TrialConditions.MenuCondition));
            var meatConditions = Enum.GetValues(typeof(TrialConditions.MeatCondition));
            var vegeConditions = Enum.GetValues(typeof(TrialConditions.VegeCondition));

            foreach (var education in educationConditions)
            {
                foreach (var menu in menuTypeConditions)
                {
                    foreach (var meat in meatConditions)
                    {
                        foreach (var vege in vegeConditions)
                        {
                            conditions.Add(new TrialConditions(
                                (TrialConditions.EducationCondition)education,
                                (TrialConditions.MenuCondition)menu,
                                (TrialConditions.MeatCondition)meat,
                                (TrialConditions.VegeCondition)vege
                            ));
                        }
                    }
                }
            }

            return conditions;
        }

        private static T GetRandom<T>(Random random)
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}