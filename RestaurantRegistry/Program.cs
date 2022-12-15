using RestaurantRegistry.Interfases;
using RestaurantRegistry.Repositories;
using RestaurantRegistry.Services;
using System;

namespace RestaurantRegistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFoodItemRepository foodItemRepository = new FoodItemRepository();
            
            //TableRepository tableRepository = new TableRepository();
            TableOrderRepository tableOrderRepository = new TableOrderRepository();
            
            IFinancialService financialService = new FinancialService(tableOrderRepository);


            RestaurantService restaurantService = new RestaurantService(tableOrderRepository);
            restaurantService.StartService();

        }
    }
}
