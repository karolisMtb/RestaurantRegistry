using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRegistry.Models;
using RestaurantRegistry.Repositories;
using System.Collections.Generic;

namespace RestaurantRegistry.Tests
{
    [TestClass]
    public class FoodItemRepositoryTests
    {
        [TestMethod]
        public void ListContainsFewFoodItemsWhenReturned()
        {
            //Arrange
            FoodItemRepository foodItemRepository = new FoodItemRepository();

            int numOfItems = 2;

            //Act
            var list = foodItemRepository.OrderDrinks(numOfItems);
            int listLength = list.Count;


            //Assert
            Assert.AreEqual(numOfItems, listLength);
        }
        
        [TestMethod]

        public void ListContainsFewMealItemsWhenReturned()
        {
            //Arrange
            FoodItemRepository foodItemRepository = new FoodItemRepository();

            int numberOfMeals = 5;

            //Act
            List<MealItem> mealItems = foodItemRepository.OrderMeals(numberOfMeals);
            int listLength = mealItems.Count;

            //Assert
            Assert.AreEqual(mealItems.Count, listLength);

        }
    }

}
