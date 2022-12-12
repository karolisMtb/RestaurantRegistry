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
            
            TableRepository tableRepository = new TableRepository();
            TableOrderRepository tableOrderRepository = new TableOrderRepository();
            TableOrderGenerator tableOrderGenerator = new TableOrderGenerator(foodItemRepository, tableOrderRepository);

            IFinancialService financialService = new FinancialService(tableOrderRepository);

            RestaurantService restaurantService = new RestaurantService(foodItemRepository, tableOrderGenerator, tableRepository, tableOrderRepository);
            restaurantService.StartService();

            var allOrders = tableOrderRepository.allOrders; // tikrinti kodel i tableOrderRepository neateina tableOrder
        }
    }
}
