using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRegistry.Services;

namespace RestaurantRegistry.Tests
{
    [TestClass]
    public class RestaurantServiceTests
    {
        [TestMethod]
        public void ReturnsNumberOfTablesNeededBasedOnCustomerCount()
        {
            // Arrange
            RestaurantService service = new RestaurantService();
            int expected = 3;
            int customerCount = 10;

            //Act
            int tablesCountNeeded = service.GetTableCountNeeded(customerCount);

            //Assert
            Assert.AreEqual(expected, tablesCountNeeded);
        }  
    }
}
