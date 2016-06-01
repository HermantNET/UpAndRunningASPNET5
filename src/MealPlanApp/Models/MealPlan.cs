using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    public class MealPlan
    {
        public long MealPlanID { get; set; }

        public string Author { get; set; }

        public double TotalCalories { get; set; }

        public double Carbohydrates { get { return MacroRatio[0]; } set { MacroRatio[0] = value; } }
        public double Proteins { get { return MacroRatio[1]; } set { MacroRatio[1] = value; } }
        public double Fats { get { return MacroRatio[2]; } set { MacroRatio[2] = value; } }

        public double[] MacroRatio = { 40, 30, 30 };

        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string Snack { get; set; }

        public string Plan = "Banana~~Cereal**PB&J**Steak~~Beans**Carrot~~Candy";
    }
}
