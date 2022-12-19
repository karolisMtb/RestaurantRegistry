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
