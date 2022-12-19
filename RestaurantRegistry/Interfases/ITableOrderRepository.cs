using RestaurantRegistry.Models;
using System;
using System.Collections.Generic;

namespace RestaurantRegistry.Interfases
{
    public interface ITableOrderRepository
    {
        DateTime GetTableCloseTime(Table table);
        Guid GetOrderNumber(Table table);
        List<TableOrder> GetTableOrders(Table table);
        double GetTablesAmountToPay();
    }
}
