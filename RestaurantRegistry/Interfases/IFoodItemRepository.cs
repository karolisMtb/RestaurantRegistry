using RestaurantRegistry.Models;
using System.Collections.Generic;

namespace RestaurantRegistry.Interfases
{
    public interface IFoodItemRepository
    {
        List<FoodItem> OrderDrinks(int numberOfItems);
        List<FoodItem> OrderMeals(int numberOfItems);
    }
}
