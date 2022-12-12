using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Models
{
    public class MealItem : FoodItem
    {
        public const double taxRate = 0.79;
        public override double Profit
        {
            get
            {
                return SalePrice * taxRate;
            }
        }
    }
}
