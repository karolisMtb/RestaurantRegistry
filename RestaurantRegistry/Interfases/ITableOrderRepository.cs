using RestaurantRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRegistry.Interfases
{
    public interface ITableOrderRepository
    {
        DateTime GetTableCloseTime(Table table);
        Guid GetOrderNumber(Table table);
        List<TableOrder> GetTableOrders(Table table);
        double GetTableAmountToPay(Table table);
    }
}
