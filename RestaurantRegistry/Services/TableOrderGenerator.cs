using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System;

namespace RestaurantRegistry.Services
{
    public class TableOrderGenerator
    {
        IFoodItemRepository foodItemRepository;
        TableOrderRepository tableOrderRepository;

        public TableOrderGenerator(IFoodItemRepository foodItemRepository, TableOrderRepository tableOrderRepository)
        {
            this.foodItemRepository = foodItemRepository;
            this.tableOrderRepository = tableOrderRepository;
        }

        public TableOrder GenerateTableOrder(Table table, Guid tableOrderNumber) // perduodu table kaip parametra. Jis turi Orders list
        {                                                 // vienas table turi tureti viena Guid() number     
            Console.WriteLine("Generating order..");

            int randomFoodItemCount = new Random().Next(1, 2);
            TableOrder tableOrder = new TableOrder(table.Number, tableOrderNumber);

            tableOrder.foodItems.AddRange(foodItemRepository.OrderDrinks(randomFoodItemCount));
            tableOrder.foodItems.AddRange(foodItemRepository.OrderMeals(randomFoodItemCount));

            tableOrderRepository.allOrders.Add(tableOrder);
            Console.WriteLine("Generated order");

            return tableOrder;
        }
    }
}
