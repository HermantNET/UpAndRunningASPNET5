using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    public class Food
    {
        public long FoodID { get; set; }

        public string Author { get; set; }

        [Required]
        public string Name { get; set; }

        public int Calories { get; set; }


        public bool Carbohydrate { get; set; }

        public bool Protein { get; set; }

        public bool Fat { get; set; }


        public bool Breakfast { get; set; }

        public bool Lunch { get; set; }

        public bool Dinner { get; set; }

        public bool Snack { get; set; }
    }
}
