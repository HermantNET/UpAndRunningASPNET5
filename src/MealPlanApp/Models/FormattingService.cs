using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    public class FormattingService
    {

        public string[] TildeToArray(string rose)
        {
            var roseBud = rose.Split(new string[] { "~~" }, StringSplitOptions.None);

            return roseBud;
        }

        public string[] AsteriskToArray(string sapling)
        {
            var oak = sapling.Split(new string[] { "**" }, StringSplitOptions.None);

            return oak;
        }
    }
}
