using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    public class MealPlanCalculator
    {
        public List<Food> Foods { get; set; }

        //check to make sure macros add up to at most 100
        public static double[] ratioCheck(double[] macroRatio)
        {
            double total = 0;
            foreach (double m in macroRatio) { total += m; }
            while (total > 100)
            {
                for (var i = 0; i < 3; i++)
                {
                    macroRatio[i] -= 1;
                    total--;
                    if (total <= 100) { break; }
                }
            }

            return macroRatio;
        }

        public string[] MyMealPlan(double TotalCalories, double[] macroRatio)
        {
            {
                string[] mealPlan = { "F", "A" };

                if (Foods != null)
                {
                    double[] total = new double[2];

                    for (int i = 0; i < 3; i++)
                    {
                        total[i] = TotalCalories / macroRatio[i];
                    }

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    total[i]
                    //}
                }
                    return mealPlan;
            }
        }
    }
}
