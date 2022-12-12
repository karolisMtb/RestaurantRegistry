using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRegistry.Repositories;

namespace RestaurantRegistry.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListContainsFewFoodItemsWhenReturned()
        {
            // Arrange
            FoodItemRepository foodItemRepository = new FoodItemRepository();

            int numOfItems = 2;

            // Act
            var list = foodItemRepository.OrderDrinks(2);
            int listLength = list.Count;


            // Assert
            Assert.AreEqual(numOfItems, listLength);
        }
    }
}
