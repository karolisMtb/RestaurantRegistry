using RestaurantRegistry.Models;
using System.Collections.Generic;

namespace RestaurantRegistry.Interfases
{
    public interface IRestaurantService
    {
        void StartService();
        int GenerateRandomCustomerCount();
        int GetTableCountNeeded(int customerNumber);
        int GetFreeSeatsCount();
        List<Table> FindAvailableSeats(int numOfPeopleToBeSeated, int tableCountNeeded);
        void SeatCustomer(int numOfCustomersToBeSeated, Table table);
        void TakeOrders();
        void WriteRestraurantReportToFile();
        void SendReportToEmail();
    }
}
