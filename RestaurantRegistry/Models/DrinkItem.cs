namespace RestaurantRegistry.Models
{
    public class DrinkItem : FoodItem
    {
        public const double taxRate = 0.91;
        public override double Profit
        {
            get
            {
                return SalePrice * taxRate;
            }
        }
    }
}
