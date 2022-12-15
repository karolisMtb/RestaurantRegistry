using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;

namespace RestaurantRegistry.Services
{
    public class TableOrderGenerator
    {
        IFoodItemRepository foodItemRepository;

        public TableOrderGenerator(IFoodItemRepository foodItemRepository)
        {
            this.foodItemRepository = foodItemRepository;
        }

        public TableOrder GenerateTableOrder(Table table, Guid tableOrderNumber)
        {  
            Console.WriteLine($"Generating order... for table {table.Number}");

            int randomFoodItemCount = new Random().Next(1, 2);
            TableOrder tableOrder = new TableOrder(table.Number, tableOrderNumber);

            tableOrder.foodItems.AddRange(foodItemRepository.OrderDrinks(randomFoodItemCount));
            tableOrder.foodItems.AddRange(foodItemRepository.OrderMeals(randomFoodItemCount));
            tableOrder.IsPaid = false;

            Console.WriteLine("Generated order");

            return tableOrder;
        }
    }
}
