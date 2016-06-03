using MealPlanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    //public class FoodsSummary
    //{
    //    public string Name { get; set; }
    //    public double Calories { get; set; }
    //    public bool Breakfast { get; set; }
    //    public bool Lunch { get; set; }
    //    public bool Dinner { get; set; }
    //    public bool Snack { get; set; }
    //}

    public class MealPlanCalculator
    {
        //private static MealDbContext _db;

        //public void DatabaseContext(MealDbContext db)
        //{
        //    _db = db;
        //}

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

        public static string MyMealPlan(double TotalCalories, double[] macroRatio, string UserIdentity)
        {
            {
                MealDbContext _db = new MealDbContext();

                double[] total = new double[4];
                double[,] macros = new double[4, 3];

                //get calorie totals for each meal
                for (int i = 0; i < 3; i++)
                {
                    total[i] = (TotalCalories / 3) - 100;
                }
                total[3] = 300;

                //divide meal portions into macros

                //Loop through each meal time
                for (int MealTime = 0; MealTime < 4; MealTime++)
                {
                    //assign macros to each meal time
                    for (int macro = 0; macro < 3; macro++)
                    {
                        macros[MealTime, macro] = total[MealTime] * (macroRatio[macro] / 100);
                    }
                }

                //Create arrays of foods availabe to certain times of days
                var Breakfast = _db.Foods.Where(y => y.Breakfast == true && y.Author == UserIdentity).ToArray();
                var Lunch = _db.Foods.Where(y => y.Lunch == true && y.Author == UserIdentity).ToArray();
                var Dinner = _db.Foods.Where(y => y.Dinner == true && y.Author == UserIdentity).ToArray();
                var Snack = _db.Foods.Where(y => y.Snack == true && y.Author == UserIdentity).ToArray();

                Random rand = new Random();
                int upperBound = Breakfast.Count();

                string result = string.Empty;
                int x = 0;

                if (!(Breakfast.Count() == 0))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        while (macros[0, i] > 0)
                        {
                            x = rand.Next(upperBound);

                            switch (i)
                            {
                                case 0:
                                    if (Breakfast[x].Carbohydrate) { macros[0, i] -= Breakfast[x].Calories; } else { continue; }
                                    break;
                                case 1:
                                    if (Breakfast[x].Protein) { macros[0, i] -= Breakfast[x].Calories; } else { continue; }
                                    break;
                                case 2:
                                    if (Breakfast[x].Fat) { macros[0, i] -= Breakfast[x].Calories; } else { continue; }
                                    break;
                            }

                            result += Breakfast[x].Name.ToString() + "~~";
                        }
                    }

                }

                result = result.Substring(0, result.Length - 2);
                result += "**";

                //LUNCH
                upperBound = Lunch.Count();

                if (!(Lunch.Count() == 0))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        while (macros[1, i] > 0)
                        {
                            x = rand.Next(upperBound);

                            switch (i)
                            {
                                case 0:
                                    if (Lunch[x].Carbohydrate) { macros[1, i] -= Lunch[x].Calories; } else { continue; }
                                    break;
                                case 1:
                                    if (Lunch[x].Protein) { macros[1, i] -= Lunch[x].Calories; } else { continue; }
                                    break;
                                case 2:
                                    if (Lunch[x].Fat) { macros[1, i] -= Lunch[x].Calories; } else { continue; }
                                    break;
                            }

                            result += Lunch[x].Name.ToString() + "~~";
                        }
                    }

                    result = result.Substring(0, result.Length - 2);
                    result += "**";

                    //DINNER
                    upperBound = Dinner.Count();

                    if (!(Dinner.Count() == 0))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            while (macros[2, i] > 0)
                            {
                                x = rand.Next(upperBound);

                                switch (i)
                                {
                                    case 0:
                                        if (Dinner[x].Carbohydrate) { macros[2, i] -= Dinner[x].Calories; } else { continue; }
                                        break;
                                    case 1:
                                        if (Dinner[x].Protein) { macros[2, i] -= Dinner[x].Calories; } else { continue; }
                                        break;
                                    case 2:
                                        if (Dinner[x].Fat) { macros[2, i] -= Dinner[x].Calories; } else { continue; }
                                        break;
                                }

                                result += Dinner[x].Name.ToString() + "~~";
                            }
                        }

                        result = result.Substring(0, result.Length - 2);
                        result += "**";

                        //SNACK
                        upperBound = Snack.Count();

                        if (!(Snack.Count() == 0))
                            for (int i = 0; i < 3; i++)
                            {
                                while (macros[3, i] > 0)
                                {
                                    x = rand.Next(upperBound);

                                    switch (i)
                                    {
                                        case 0:
                                            if (Snack[x].Carbohydrate) { macros[3, i] -= Snack[x].Calories; } else { continue; }
                                            break;
                                        case 1:
                                            if (Snack[x].Protein) { macros[3, i] -= Snack[x].Calories; } else { continue; }
                                            break;
                                        case 2:
                                            if (Snack[x].Fat) { macros[3, i] -= Snack[x].Calories; } else { continue; }
                                            break;
                                    }

                                    result += Snack[x].Name.ToString() + "~~";
                                }
                            }
                    }
                }
                result = result.Substring(0, result.Length - 2);

                return result;
            }
        }
    }
}