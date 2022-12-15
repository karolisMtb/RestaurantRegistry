using RestaurantRegistry.Models;
using System.Collections.Generic;

namespace RestaurantRegistry.Interfases
{
    public interface IFoodItemRepository
    {
        List<DrinkItem> OrderDrinks(int numberOfItems);
        List<MealItem> OrderMeals(int numberOfItems);
    }
}
