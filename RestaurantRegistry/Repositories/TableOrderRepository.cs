using RestaurantRegistry.Models;
using System.Collections.Generic;

namespace RestaurantRegistry.Repositories
{
    public class TableOrderRepository
    {
        public List<TableOrder> allOrders;

        public TableOrderRepository()
        {
            allOrders = new List<TableOrder>();
        }
    }
}
