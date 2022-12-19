using LINQtoCSV;
using RestaurantRegistry.Interfases;
using RestaurantRegistry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RestaurantRegistry.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        public List<FoodItem> foodItems = new List<FoodItem>();
        public List<MealItem> mealItems = new List<MealItem>();
        public List<DrinkItem> drinkItems = new List<DrinkItem>();

        private string path = @"C:\Users\Karolis\source\repos\RestsurantRegistryApp\RestaurantRegistry\Files";
        public FoodItemRepository ()
        {

        }
        public List<DrinkItem> OrderDrinks(int numberOfItems)
        {
            string fileExtension = @"\Drinks.csv";

            var csvFileDescription = new CsvFileDescription()
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            CsvContext csvContext = new CsvContext();

            try
            {
                var DB_drinksList = csvContext.Read<DrinkItem>((path + fileExtension), csvFileDescription);
           
                for(int i = 0; i < numberOfItems; i++)
                {
                    int randomFoodItem = new Random().Next(0, DB_drinksList.Count());
                    drinkItems.Add(DB_drinksList.ToList()[randomFoodItem]);
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("File for drinks was not found");
            }

            return drinkItems;
        }
        public List<MealItem> OrderMeals(int numberOfItems)
        {
            string fileExtension = @"\Meals.csv";

            var csvFileDescription = new CsvFileDescription()
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            CsvContext csvContext = new CsvContext();

            try
            {
                var DB_mealsList = csvContext.Read<MealItem>((path + fileExtension), csvFileDescription);
                for (int i = 0; i < numberOfItems; i++)
                {
                    int randomFoodItem = new Random().Next(0, DB_mealsList.Count());
                    mealItems.Add(DB_mealsList.ToList()[randomFoodItem]);
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("File for meals was not found");
            }

            return mealItems;
        }
    }

    
}
